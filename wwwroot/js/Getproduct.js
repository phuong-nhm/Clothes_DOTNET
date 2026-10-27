$(document).ready(function () {
    // Only load products if we're on AllProduct page
    if (window.location.pathname.includes('/Sale/AllProduct')) {
        var urlParams = new URLSearchParams(window.location.search);
        var searchname = urlParams.get('searchname') || "";
        var category = urlParams.get('category') || "";
        var claim = urlParams.get('claim') || null;
        var gender = urlParams.get('gender') || "";
        var sale = urlParams.get('sale') || "";
        var sortby = urlParams.get('sortby') || "";
        
        LoadData(searchname, category, claim, gender, sale, sortby, 1, 12);
    }

    // Search functionality
    $('body').on('click', '#btnsearch', function () {
        var txtsearch = $('#txtsearch').val();
        window.location.href = "/Sale/AllProduct?searchname=" + encodeURIComponent(txtsearch);
    });

    // Category filter
    $('body').on('click', '.drop-category', function () {
        var category = $(this).data('category');
        window.location.href = `/Sale/AllProduct?category=${encodeURIComponent(category)}`;
    });

    // Claim filter
    $('body').on('click', '.drop-claim', function () {
        var claim = $(this).data('claim');
        window.location.href = `/Sale/AllProduct?claim=${encodeURIComponent(claim)}`;
    });

    // Gender filter
    $('body').on('click', '.gender', function () {
        var gender = $(this).data('gender');
        window.location.href = `/Sale/AllProduct?gender=${encodeURIComponent(gender)}`;
    });

    // Sale filter
    $('body').on('click', '.sale', function () {
        var sale = $(this).data('sale');
        window.location.href = `/Sale/AllProduct?sale=${encodeURIComponent(sale)}`;
    });
});

/**
 * Load and display product data
 */
function LoadData(searchname, category, claim, gender, sale, sortby, page, pagesize) {
    pagesize = pagesize || 12;
    page = page || 1;
    
    console.log("Loading products...", { searchname, category, claim, gender, sale, page, pagesize });
    
    $.ajax({
        url: '/Sale/GetAllProduct',
        type: 'GET',
        data: {
            searchname: searchname || "",
            category: category || "",
            claim: claim || null,
            gender: gender || "",
            sale: sale || "",
            sortby: sortby || "",
            page: page,
            pagesize: pagesize
        },
        success: function (res) {
            console.log("API Response:", res);
            
            if (!res || res.totalItems === 0) {
                $('#show_data').html('<div class="col-12 text-center py-5"><p class="text-muted fs-5">Không tìm thấy sản phẩm nào phù hợp.</p></div>');
                $('#pagination').html("");
                return;
            }

            // API uses JSON.NET reference preservation - data is wrapped in $values
            var listid = res.objectsale && res.objectsale.$values ? res.objectsale.$values : [];
            var items = res.data && res.data.$values ? res.data.$values : [];
            var html = "";

            console.log("Products:", items);

            if (!items || items.length === 0) {
                $('#show_data').html('<div class="col-12 text-center py-5"><p class="text-muted fs-5">Không tìm thấy sản phẩm nào.</p></div>');
                $('#pagination').html("");
                return;
            }

            for (let i = 0; i < items.length; i++) {
                var product = items[i];
                
                if (!product) continue;

                var tempsale = 0;
                var hasSale = false;

                // Check if product has sale (API uses camelCase: id, sellprice, etc.)
                for (let j = 0; j < listid.length; j++) {
                    if (product.id === listid[j].productId) {
                        hasSale = true;
                        tempsale = listid[j].discountpercentage;
                        break;
                    }
                }

                var badgeHtml = hasSale ? `<span class="badge bg-danger position-absolute top-0 start-0 m-2 z-1">-${tempsale}%</span>` : '';
                var imageSrc = product.image ? (product.image.startsWith('/') ? product.image : '/UserImage/images/' + product.image) : '/UserImage/images/noimage.jpg';
                var priceDisplay = '';
                var price = product.sellprice || 0;

                if (hasSale && price > 0) {
                    var discountedPrice = (price * (100 - tempsale)) / 100;
                    priceDisplay = `
                        <div class="d-flex align-items-baseline gap-2">
                            <span class="fw-bold text-danger fs-6">${Math.round(discountedPrice).toLocaleString('vi-VN')}đ</span>
                            <small class="text-muted text-decoration-line-through">${Math.round(price).toLocaleString('vi-VN')}đ</small>
                        </div>`;
                } else if (price > 0) {
                    priceDisplay = `<span class="fw-bold text-dark fs-6">${Math.round(price).toLocaleString('vi-VN')}đ</span>`;
                } else {
                    priceDisplay = `<span class="fw-bold text-dark fs-6">Liên hệ</span>`;
                }

                html += `
                <div class="col">
                    <div class="card h-100 border-0 shadow-sm rounded-3 overflow-hidden position-relative">
                        ${badgeHtml}
                        <a href="/Sale/Get_Product_Detail?id=${product.id}" class="text-decoration-none">
                            <div style="overflow: hidden; height: 280px;">
                                <img src="${imageSrc}" alt="${product.name}" class="w-100 h-100 object-fit-cover" style="transition: transform 0.4s ease; cursor: pointer;" onmouseover="this.style.transform='scale(1.08)'" onmouseout="this.style.transform='scale(1)'">
                            </div>
                        </a>
                        <div class="card-body d-flex flex-column p-3">
                            <h6 class="card-title fw-semibold text-dark text-truncate mb-2" title="${product.name}">
                                <a href="/Sale/Get_Product_Detail?id=${product.id}" class="text-dark text-decoration-none">${product.name}</a>
                            </h6>
                            <div class="mt-auto mb-3">
                                ${priceDisplay}
                            </div>
                            <a href="/Sale/Get_Product_Detail?id=${product.id}" class="btn btn-dark btn-sm w-100 rounded-pill fw-medium">
                                Xem chi tiết
                            </a>
                        </div>
                    </div>
                </div>`;
            }

            $('#show_data').html(html);
            GeneratePagination(res.currentPage, res.numberpage, res.pagesize);
        },
        error: function (xhr, status, error) {
            console.error("AJAX Error:", status, error);
            console.error("Response:", xhr.responseText);
            $('#show_data').html('<div class="col-12 text-center py-5"><p class="text-danger">Lỗi khi tải sản phẩm: ' + error + '</p></div>');
        }
    });
}

/**
 * Generate pagination
 */
function GeneratePagination(currentPage, totalPages, pagesize) {
    if (!totalPages || totalPages <= 0) return;

    var paginationHtml = `<nav aria-label="Page navigation"><ul class="pagination gap-2">`;

    // Previous button
    if (currentPage > 1) {
        paginationHtml += `<li class="page-item"><a class="page-link rounded-3 border-0 bg-light text-dark fw-medium" href="javascript:void(0)" onclick="NextPage(${currentPage - 1}, ${pagesize})">← Trước</a></li>`;
    }

    // Page numbers
    for (let i = 1; i <= totalPages; i++) {
        if (i === currentPage) {
            paginationHtml += `<li class="page-item"><span class="page-link rounded-3 border-0 bg-dark text-white fw-medium">${i}</span></li>`;
        } else {
            paginationHtml += `<li class="page-item"><a class="page-link rounded-3 border-0 bg-light text-dark fw-medium" href="javascript:void(0)" onclick="NextPage(${i}, ${pagesize})">${i}</a></li>`;
        }
    }

    // Next button
    if (currentPage < totalPages) {
        paginationHtml += `<li class="page-item"><a class="page-link rounded-3 border-0 bg-light text-dark fw-medium" href="javascript:void(0)" onclick="NextPage(${currentPage + 1}, ${pagesize})">Sau →</a></li>`;
    }

    paginationHtml += `</ul></nav>`;
    $('#pagination').html(paginationHtml);
}

/**
 * Handle pagination click
 */
function NextPage(page, pagesize) {
    var urlParams = new URLSearchParams(window.location.search);
    LoadData(
        urlParams.get('searchname') || "",
        urlParams.get('category') || "",
        urlParams.get('claim') || null,
        urlParams.get('gender') || "",
        urlParams.get('sale') || "",
        urlParams.get('sortby') || "",
        page,
        pagesize
    );
    
    // Scroll to top
    window.scrollTo(0, 0);
}