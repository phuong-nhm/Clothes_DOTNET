
$('.nav-item.dropdown').hover(
    function () {
        $(this).find('.dropdown-menu').addClass('show');

    },
    function () {
        $(this).removeClass('show');
        $(this).find('.dropdown-menu').removeClass('show');
    }


);
    var name = "";
    $.ajax({
        url: '/Home/getcategory', // Đường dẫn tới action
        type: 'GET',
        dataType: 'json',
        success: function (res) {
            var dropdown = $("#categoryDropdown");
            dropdown.empty();
    
            // Lấy danh sách categories và category claims từ response
            var categories = res.categoris.$values;
            var categoryClaims = res.categoryclaims.$values;
    
            console.log(categories);
            console.log(categoryClaims);
    
            var listHtml = ``;
    
            // Duyệt qua từng nhóm category_claim
            categoryClaims.forEach(function (claim) {
                listHtml += `<li><a class="dropdown-item drop-category" href="#">${claim.claimName}</a>`;
                listHtml += `<div><ul>`;
    
                // Duyệt qua các category thuộc về nhóm này
                categories.forEach(function (category) {
                    if (category.category_claim_Id == claim.id) {
                        listHtml += `<li><a class="dropdown-item drop-claim" href="#">${category.name}</a></li>`;
                    }
                });
    
                listHtml += `</ul></div></li>`;
            });
    
            dropdown.append(listHtml); // Gắn danh sách HTML vào dropdown
        },
        error: function () {
            alert("Failed to load categories.");    
        }
    });
    