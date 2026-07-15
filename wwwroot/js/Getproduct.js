$(document).ready(function () {

    var urlParams = new URLSearchParams(window.location.search);
    var searchname = urlParams.get('searchname') || "";
    var category = urlParams.get('category') || "";
    var claim = urlParams.get('claim') || null;
    var gender = urlParams.get('gender') || "";
    var sale = urlParams.get('sale') || "";
    var sortby = urlParams.get('sortby') || "";
    LoadData(searchname, category, claim,gender,sale);
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
function LoadData(searchname, category, claim, gender, sale, sortby="",page,pagesize) {
    $.ajax({
        url: '/Sale/GetAllProduct',
        method: 'GET',
        data: { searchname: searchname, category: category, claim: claim, gender: gender, sale: sale, sortby: sortby,page: page, pagesize: 12},
        success: function (res) {
            console.log("Res: ", res);
            if (res.totalItems === 0) {
                $('#show_data').html('<p>Không tìm thấy sản phẩm nào.</p>');
                $('#pagination').html("");
            }
            else {
                var listid = res.objectsale.$values;

                console.log("Vao duoc roi", listid);

                var items = res.data.$values ;
                var html = "";
                for (let i = 0; i < items.length; i++) {
                
                    var tempsale = 0;
                    var bol = false;
                    html += "<div class='product-item col-6 col-md-2'>";
                    html += "<a class='product-link'>";
                    html += "<a href='/Sale/Get_Product_Detail?id=" + items[i].id + "'>";
                    if (items[i].image) {
                        // Tạo đường dẫn đến ảnh
                        var imageSrc = '/UserImage/images/' + items[i].image;  // Đảm bảo rằng 'items[i].image' chứa tên file ảnh, ví dụ 'anh1.jpg'
                        html += "<img src='" + imageSrc + "' alt='"+ items[i].name + "' class='product-image'/>";
                    } else {
                        html += "<img src='anh_1_test.jpg' alt='Default Image' class='product-image' />";
                    }
                    html += "</a>";
                    html += "</a>";
                    html += "<h2 class='product-name'>" + items[i].name + "</h2>";
                    var temp = items[i].id;
                    console.log("Gia tri:", temp);
                    for (let j = 0; j < listid.length; j++) {
                        var restemp = listid[j];
                        if (temp === restemp.productId) {
                            bol = true;
                            tempsale = restemp.discountpercentage; 
                            break; 
                        }
                    }
                    if (bol) {
                        var price = (items[i].sellprice * tempsale) / 100;
                        html += "<p class='product-price'> Giá Cũ " + items[i].sellprice.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',') + "đ</p>";
                        html += "<p class='product-price'> Giá Mới " + price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',') + "đ</p>";
                    }
                    else {
                        html += "<p class='product-price'>" + items[i].sellprice.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',') + "đ</p>";
                    }
                    html += "<button class='buy-button' onclick=\"window.location='/Sale/Get_Product_Detail?id=" + items[i].id + "'\">";
                    html += "<i class='fas fa-shopping-cart'></i> Mua ngay";
                    html += "</button>";
                    html += "</div>";
                }
                $('#show_data').html(html);
                Pagination(res.currentPage, res.numberpage, res.pagesize);
            }
        },
        error: function (xhr, status, error) {
            console.error("AJAX Error:", status, error);
            console.error("Response Text:", xhr.responseText);
        }
    });
    
}
function Pagination(currentPage, NumberPage,Pagesize) {
    if (NumberPage > 0) {
        var str = `<nav aria-label="Page navigation example"> <ul class="pagination">`;
        if (currentPage > 1) {
            str += `<li class="page-item "><a class="page-link" onclick="NextPage(${currentPage - 1},${Pagesize})" href="javascript:void(0)">Previous</a></li>`;
        }
        for (let i = 1; i <= NumberPage; i++) {
            if (currentPage == i) {
                str += `<li class="page-item active"><a class="page-link" href="javascript:void(0)">${i}</a></li>`;
            }
            else {
                str += `<li class="page-item "><a class="page-link" onclick="NextPage(${i},${Pagesize})" href="javascript:void(0)">${i}</a></li>`;
            }

        }
        if (NumberPage > 1) {
            str += `<li class="page-item "><a class="page-link" onclick="NextPage(${currentPage + 1},${Pagesize})" href="javascript:void(0)">Next</a></li>`;
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