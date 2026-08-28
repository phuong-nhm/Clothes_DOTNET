$(document).ready(function () {
    var urlParams = new URLSearchParams(window.location.search);
    var searchname = urlParams.get('searchname') || "";
    var category = urlParams.get('category') || "";
    var claim = urlParams.get('claim') || null;
    var gender = urlParams.get('gender') || "";
    var sale = urlParams.get('sale') || "";
    var sortby = urlParams.get('sortby') || "";

    LoadData(searchname, category, claim, gender, sale);

    $('body').on('click', '#btnsearch', function () {
        var txtsearch = $('#txtsearch').val();
        window.location.href = "/Sale/AllProduct?searchname=" + encodeURIComponent(txtsearch);
    });

    $('body').on('click', '.drop-category', function () {
        var category = $(this).data('category');
        window.location.href = `/Sale/AllProduct?category=${encodeURIComponent(category)}`;
    });

    $('body').on('click', '.drop-claim', function () {
        var claim = $(this).data('claim');
        window.location.href = `/Sale/AllProduct?claim=${encodeURIComponent(claim)}`;
    });

    $('body').on('click', '.gender', function () {
        var gender = $(this).data('gender');
        window.location.href = `/Sale/AllProduct?gender=${encodeURIComponent(gender)}`;
    });

    $('body').on('click', '.sale', function () {
        var sale = $(this).data('sale');
        window.location.href = `/Sale/AllProduct?sale=${encodeURIComponent(sale)}`;
    });

    $('#category').on('change', function () {
        var sortby = $(this).val();
        window.location.href = `/Sale/AllProduct?category=${encodeURIComponent(params.category)}&claim=${encodeURIComponent(params.claim)}&gender=${encodeURIComponent(params.gender)}&sortby=${encodeURIComponent(params.sortby)}`;
    });
});

function LoadData(searchname, category, claim, gender, sale, sortby = "", page, pagesize) {
    $.ajax({
        url: '/Sale/GetAllProduct',
        method: 'GET',
        data: { searchname: searchname, category: category, claim: claim, gender: gender, sale: sale, sortby: sortby, page: page, pagesize: 12 },
        success: function (res) {
            if (res.totalItems === 0) {
                $('#show_data').html('<div class="col-12 text-center py-5"><p class="text-muted fs-5">Không tìm thấy sản phẩm nào phù hợp.</p></div>');
                $('#pagination').html("");
                return;
            }

            var listid = res.objectsale.$values || [];
            var items = res.data.$values || [];
            var html = "";

            for (let i = 0; i < items.length; i++) {
                var tempsale = 0;
                var bol = false;

                // Kiểm tra sale
                for (let j = 0; j < listid.length; j++) {
                    if (items[i].id === listid[j].productId) {
                        bol = true;
                        tempsale = listid[j].discountpercentage;
                        break;
                    }
                }

                var badgeHtml = bol ? `<span class="badge bg-danger position-absolute top-0 start-0 m-2 z-1">-${tempsale}%</span>` : '';
                var imageSrc = items[i].image ? ('/UserImage/images/' + items[i].image) : 'anh_1_test.jpg';
                var priceHtml = '';

                if (bol) {
                    var price = (items[i].sellprice * (100 - tempsale)) / 100;
                    priceHtml = `
                        <div class="d-flex align-items-baseline gap-2">
                            <span class="fw-bold text-danger fs-6">${price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, '.')}đ</span>
                            <small class="text-muted text-decoration-line-through">${items[i].sellprice.toString().replace(/\B(?=(\d{3})+(?!\d))/g, '.')}đ</small>
                        </div>`;
                } else {
                    priceHtml = `<span class="fw-bold text-dark fs-6">${(items[i].sellprice ? git add.git add.?0).toString().replace(/\B(?=(\d{3})+(?!\d))/g, '.')}đ</span>`;
                }

                html += `
                <div class="col">
                    <div class="card h-100 border-0 shadow-sm rounded-3 overflow-hidden position-relative product-card">
                        ${badgeHtml}
                        <a href="/Sale/Get_Product_Detail?id=${items[i].id}" class="text-decoration-none">
                            <img src="${imageSrc}" alt="${items[i].name}" class="card-img-top object-fit-cover" style="height: 220px;" />
                        </a>
                        <div class="card-body d-flex flex-column p-3">
                            <h6 class="card-title fw-semibold text-dark text-truncate mb-2" title="${items[i].name}">
                                <a href="/Sale/Get_Product_Detail?id=${items[i].id}" class="text-dark text-decoration-none">${items[i].name}</a>
                            </h6>
                            <div class="mt-auto mb-3">
                                ${priceHtml}
                            </div>
                            <button class="btn btn-outline-dark btn-sm w-100 rounded-pill mt-auto fw-medium d-flex align-items-center justify-content-center gap-2" onclick="window.location='/Sale/Get_Product_Detail?id=${items[i].id}'">
                                <i class="fas fa-shopping-cart"></i> Xem chi tiết
                            </button>
                        </div>
                    </div>
                </div>`;
            }

            $('#show_data').html(html);
            Pagination(res.currentPage, res.numberpage, res.pagesize);
        },
        error: function (xhr, status, error) {
            console.error("AJAX Error:", status, error);
        }
    });
}

function Pagination(currentPage, NumberPage, Pagesize) {
    if (NumberPage > 0) {
        var str = `<nav aria-label="Page navigation" class="d-flex justify-content-center mt-4"> <ul class="pagination pagination-sm gap-1">`;
        if (currentPage > 1) {
            str += `<li class="page-item"><a class="page-link rounded-circle px-3 text-dark border-0 shadow-sm" onclick="NextPage(${currentPage - 1},${Pagesize})" href="javascript:void(0)">&laquo;</a></li>`;
        }
        for (let i = 1; i <= NumberPage; i++) {
            if (currentPage == i) {
                str += `<li class="page-item active"><a class="page-link rounded-circle px-3 bg-dark border-0 shadow-sm" href="javascript:void(0)">${i}</a></li>`;
            }
            else {
                str += `<li class="page-item"><a class="page-link rounded-circle px-3 text-dark border-0 shadow-sm" onclick="NextPage(${i},${Pagesize})" href="javascript:void(0)">${i}</a></li>`;
            }
        }
        if (NumberPage > 1 && currentPage < NumberPage) {
            str += `<li class="page-item"><a class="page-link rounded-circle px-3 text-dark border-0 shadow-sm" onclick="NextPage(${currentPage + 1},${Pagesize})" href="javascript:void(0)">&raquo;</a></li>`;
        }
        str += "</ul> </nav>";
        $('#pagination').html(str);
    }
}

function NextPage(page, pagesize) {
    var urlParams = new URLSearchParams(window.location.search);
    var searchname = urlParams.get('searchname') || "";
    var category = urlParams.get('category') || "";
    var claim = urlParams.get('claim') || null;
    var gender = urlParams.get('gender') || "";
    var sale = urlParams.get('sale') || "";
    var sortby = urlParams.get('sortby') || "";
    LoadData(searchname, category, claim, gender, sale, sortby, page, pagesize);
}