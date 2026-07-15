function loadData(id) {
    alert("hàm load được gọi!(JS)");
    $.ajax({
        type: 'POST',
        url: '/Product/LoadProduct',    
        data: { id : id },      
        success: function (response) {
            console.log(response);
            if (response.success) {
                console.log("IDpd" + id);
                console.log("NamePd" + response.NamePd);
                const product = response.data;

                console.log("Product" + product);
                console.log("IDpd" + product.idPd);
                console.log("IDpd" + product.i);

                $("#changePopUp").show();
                $("#IdPd").val(id);
                $("#Image").attr("src", product.image);
                $("#NamePd").val(product.namePd);
                $("#IDCategory").val(product.categoryId);
                $("#SellingPrice").val(product.sellingPrice);
                $("#ImportPrice").val(product.importPrice);
                
            }
            else {
                alert(response.message);
            }
            
        },
        error: function (response) {
            //debugger;  
            console.log(xhr.responseText);
            alert("Error has occurred..");
        }
    });
}

//Thêm variant
function themVariant() {
   
    const productId = document.getElementById('productId').value;

    let selectedColors = [];
    let selectedSizes = [];
    let quantities = {};
    //lay gtri mau
    $("input[name='SelectedColors']:checked").each(function () {
        selectedColors.push($(this).val());
    });
   


    $("input[name='SelectedSizes']:checked").each(function () {
        var sizeId = $(this).val();
     
        var quantity = $('#quantity-' + $(this).attr('id').split('-')[1]).val();
        selectedSizes.push(sizeId);
        quantities[sizeId] = quantity;  // Lưu số lượng cho mỗi kích thước
    });
   
   

    //check
    if (selectedColors.length === 0 || selectedSizes.length === 0) {
        alert("Vui lòng chọn ít nhất một màu và một kích thước.");
        return false;
    }
    // Gửi dữ liệu qua AJAX
    $.ajax({
        url: '/Product/AddVariants',  
        method: 'POST',
        data: {
            productId: productId,
            selectedColors: selectedColors,
            selectedSizes: selectedSizes,
            quantities: quantities
        },
        success: function (response) {
            alert('Đã thêm biến thể thành công (AJAX)');
            // Xử lý khi thành công, ví dụ reload page hoặc update UI
        },
        error: function (xhr, status, error) {
            alert('Có lỗi xảy ra khi thêm biến thể');
        }
    });

    return false;  // Ngừng submit form
}



//Ajax thêm sản phẩm
function themProduct() {
    // Tạo đối tượng dữ liệu
    let product = {};
    let productvariant = [];
    let form = new FormData();
    // Lấy dữ liệu từ form
    let formData = $('#add-form').serializeArray();
    $.each(formData, function (index, value) {
        product[value.name] = value.value; // Mapping dữ liệu từ form
    });

    // Kiểm tra trường 'namePd'
    if (!product["NamePd"] || product["NamePd"] === "") {
        alert("Vui lòng nhập tên sản phẩm!");
        return false; // Dừng lại nếu 'namePd' rỗng
    }
    if (!product["CategoryId"] || product["CategoryId"] === "") {
        alert("Vui lòng nhập asdasdas sản phẩm!");
        return false; // Dừng lại nếu 'namePd' rỗng
    }    
      
    // Kiểm tra file ảnh
    let image = $("#imageFile").prop("files")[0];
    if (image) {
        form.append("image", image);
    } else {
        alert("Vui lòng chọn ảnh sản phẩm!");
        return false;
    }

    // Thêm dữ liệu vào FormData
    form.append("product", JSON.stringify(product));
   
    // Debugging - Kiểm tra dữ liệu
    console.log("Sản phẩm:", product);


    // Gửi dữ liệu qua AJAX
    $.ajax({
        url: '/Product/Create', // Đường dẫn tới controller
        type: 'POST',
        data: form, // Dữ liệu gửi đi
        processData: false, // Không xử lý dữ liệu (vì sử dụng FormData)
        contentType: false, // Không cần chỉ định contentType
        success: function (response) {
            $("#add-message").html(response.message);
            if (response.status) {
                $("#add-message").addClass("text-warning");
                setTimeout(function () {
                    window.location.replace("/Admin/Product");
                }, 1000)            
            } else {
                console.error("Lỗi: " + response.message);
                alert("Có lỗi xảy ra: " + response.message);
            }
        },
        error: function (xhr, status, error) {
            console.error("Lỗi:", xhr.responseText);
            alert("Có lỗi xảy ra, vui lòng thử lại!");
        }
    });

    return false; // Ngăn việc gửi form mặc định
}




//Ajax sửa sản phẩm
function suaProduct() {
    let product = {};   
    let form = new FormData();
    let formData = $('#update-form').serializeArray({
    });
    $.each(formData, function (index, value) {
        product["" + value.name + ""] = value.value;
    });
   
    let image = $("#updateImage").prop("files")[0];   
    form.append("image", image);   
    form.append("product", JSON.stringify(product));     
    $.ajax({
        url: '/Product/Update',
        type: 'POST',
        cache: false,
        processData: false,
        contentType: false,
        enctype: "multipart/form-data",
        data: form,
        async: true,
        success: function (response) {
            $("#update-message").html(response.message);
            if (response.status == true) {
                $("#update-message").addClass("text-warning");
                setTimeout(function () {
                    window.location.replace("/Admin/Product");
                }, 1000)
            } else {
                $("#update-message").addClass("text-danger");
            }
        },
        error: function (xhr, status, errore) {
            console.log(xhr.responseText);
            alert("Error has occurred..");
        }
    });
    return false;
}

function deleteData(id) {
    alert("xoa deletedata")
    $("#delete-masp").val(id);
    console.log(id);
}

function xoaSanPham() {
    alert("Xoas JS")
    let id = $("#delete-masp").val();
    console.log(id);
    $.ajax({
        type: 'POST',
        data: { "id": id },
        url: '/Product/Delete',
        success: function (response) {
            $("#delete-message").html(response.message);
            if (response.status == true) {
                $("#delete-message").addClass("text-warning");
                $(".cancelPopup").click();
                $("#row-" + id).remove();
            }
            else {
                $("#delete-message").addClass("text-danger");
            }
        },
        error: function (response) {
            console.log(xhr.responseText);
            alert("Error has occurred..");
        }
    });
}