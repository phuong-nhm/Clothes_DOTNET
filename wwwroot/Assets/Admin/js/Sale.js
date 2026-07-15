
function loadPopupProducts(page) {
    $.ajax({
        url: '/ControllerName/GetPagedProducts?page=${page}',
        type: "GET",
        success: function (data) {
            $(".input-group").html(data); // Render lại bảng danh sách sản phẩm
        },
        error: function () {
            alert("Không thể tải dữ liệu.");
        }
    });
}

// Get product from Sale
function getProduct() {
    const saleId = document.getElementById('SaleId').value; 
    $.ajax({
        url: 'Sale/getProduct',
        type: 'GET',
        data: { saleId: saleId },
        success: function (response) {
            if (response.success) {
                const product = response.data; // Lấy dữ liệu sản phẩm từ response
                $('#productName').text(product.NamePd); // Hiển thị tên sản phẩm
                $('#productImage').attr('src', product.Image); // Gán đường dẫn ảnh
            } else {
                alert(response.message); // Thông báo lỗi từ server
            }
        },
        error: function () {
            alert('Error fetching product details.'); // Thông báo lỗi kết nối
        }
    });
}
function addSale() {
    alert("ALOALO");
    const selectedProducts = [];
    document.querySelectorAll('input[type="checkbox"]:checked').forEach((checkbox) => {
        selectedProducts.push(checkbox.value);
    });

    const discountPercentage = document.getElementById('percentage').value.replace('%', '').trim();
    const startDate = document.getElementById('startDate').value;
    const endDate = document.getElementById('endDate').value;

    // Kiểm tra hợp lệ dữ liệu
    if (!selectedProducts.length) {
        alert("Vui lòng chọn ít nhất một sản phẩm.");
        return false;
    }
    if (!discountPercentage || isNaN(discountPercentage)) {
        alert("Vui lòng nhập % Sale hợp lệ.");
        return false;
    }
    if (!startDate || !endDate) {
        alert("Vui lòng chọn ngày bắt đầu và ngày kết thúc.");
        return false;
    }
    if (new Date(startDate) > new Date(endDate)) {
        alert("Ngày bắt đầu không thể sau ngày kết thúc.");
        return false;
    }
    const data = {
        productIds: selectedProducts,
        discountPercentage: parseFloat(discountPercentage),
        startDate: startDate,
        endDate: endDate
    };
    alert(endDate);
    alert(JSON.stringify(data))
    $.ajax({
        url: '/Sale/AddSale',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(data),
   
        success: function (result) {
            if (result.success) {
                alert("Thêm sale thành công! (Ajax)");
                location.reload(); // Tải lại trang
            } else {
                alert("Thêm sale thất bại: " + result.message);
            }
        },
        error: function (xhr, status, error) {
            console.error('Error:', error);
            alert("Có lỗi xảy ra.");
        }
    });


    return false; 
}

//Load data sale
function loadData(id) {
    alert("hàm load được gọi!(JS)");
    $.ajax({
        type: 'POST',
        url: '/Sale/LoadSale',
        data: { id: id },
        success: function (response) {
            console.log(response);
            if (response.success) {
                $("#updateSalePopUp").show();
                console.log("IdSale" + id);
                //console.log("NamePd" + response.NamePd);
                //const product = response.data;

                console.log("Product" + product);
                console.log("IDpd" + product.idPd);
                console.log("IDpd" + product.i);

                
               // $("#IdPd").val(id);
               // $("#Image").attr("src", product.image);
              //  $("#NamePd").val(product.namePd);
                //$("#IDCategory").val(product.categoryId);
               // $("#SellingPrice").val(product.sellingPrice);
                //$("#ImportPrice").val(product.importPrice);

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
function updateSale()
{

}

function deleteData(SaleId) {
    alert("xoa js" + SaleId);
    //document.getElementById("#delete-saleid").value = SaleId; 
    $("#delete-saleid").val(SaleId);
    console.log(SaleId);
}
function deleteSale() {
    //const saleId = document.getElementById("delete-saleid").value;
    let id = $("#delete-saleid").val();
    console.log(id);
    $.ajax({
        type: 'POST',
        data: { id: id},
        url: '/Sale/Delete',
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
            console.log(xhr.responseText);s
            alert("Error has occurred..");
        }
    });
}