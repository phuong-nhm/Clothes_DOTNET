//load dữ liệu lên form sửa
function loadData(id) {
    $.ajax({
        type: 'POST',
        url: '/Category/GetCategoryById',
        data: { "id": id },  // Gửi dữ liệu theo định dạng chuẩn
        success: function (response) {
            console.log(response); // Kiểm tra dữ liệu trả về từ server

            if (response.success) {
                // Kiểm tra các giá trị trong response.data
                console.log("CategoryClaimID: " + response.data.CategoryClaimID); // Đảm bảo truy cập đúng data
                console.log("Gender: " + response.data.Gender);

                // Gán giá trị vào các input
                $("#IDCategory").val(id);
                $("#NameCategory").val(response.data.NameCategory);  // Đảm bảo response.data
                $("#CategoryClaimID").val(response.data.CategoryClaimID);  // Gán combobox
                $("#Gender").val(response.data.Gender);  // Gán combobox

                // Hiển thị giá trị NameCategory trong thông báo
                $("#update-message").text("NameCategory: " + response.data.NameCategory);
            } else {
                alert(response.message);
            }
        },
        error: function (xhr) {
            console.error(xhr.responseText);
            alert("Đã xảy ra lỗi khi tải dữ liệu.");
        }
    });



}



//gửi ajax thêm danh mục
function themDanhMuc() {
    // Lấy dữ liệu từ form
    let data = {};
    let formData = $('#add-form').serializeArray({}); // Lấy dữ liệu từ form
    console.log(formData); // Kiểm tra dữ liệu đã được gửi hay chưa

    // Duyệt qua formData và tạo đối tượng dữ liệu cho request
    $.each(formData, function (index, value) {
        data[""+  value.name + ""] = value.value;
    });

    // Kiểm tra giá trị của NameCategory
    console.log("Tên danh mục: " + data.NameCategory); // Kiểm tra giá trị của NameCategory
     console.log("Danh muc " + JSON.stringify(data));
    // Gửi request đến server
    $.ajax({
        url: '/Category/Create',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(data),
        dataType: 'json',
        success: function (response) {
            if (response.status === true) {
                $("#add-message").addClass("text-success").removeClass("text-danger");
                $("#add-message").html(response.message);
                setTimeout(function () {
                    window.location.replace("/Admin/Category");
                }, 1000); // Chuyển hướng sau 1 giây
            } else {
                $("#add-message").addClass("text-danger").removeClass("text-success");
                $("#add-message").html(response.message);
            }
        },
        error: function (xhr, status, error) {
            console.log("Error: " + error);
            $("#add-message").addClass("text-danger").html("Đã có lỗi xảy ra. Vui lòng thử lại.");
        }
    });

    // Ngăn chặn form submit thông thường
    return false;
}


//gửi ajax sửa danh mục
function suaDanhMuc() {
    
        console.log("IDCategory: ", $("#IDCategory").val());
        console.log("NameCategory: ", $("#NameCategory").val());
        console.log("CategoryClaimID: ", $("#CategoryClaimID").val());
        console.log("Gender: ", $("#Gender").val());

      

    let data = {};
    let formData = $('#update-form').serializeArray({
    });
    $.each(formData, function (index, value) {
        data["" + value.name + ""] = value.value;
    });
    $.ajax({
        url: '/Category/Update',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(data),
        dataType: 'json',
        success: function (respone) {
            $("#update-message").html(respone.message);
            if (respone.status == true) {
                $("#update-message").addClass("text-warning");
                setTimeout(function () {
                    window.location.replace("/Admin/Category");
                }, 1000)
            } else {
                $("#update-message").addClass("text-danger");
            }
        },
        error: function (respone) {
            console.log(respone);
        }
    });
    return false;
}

//load data lên form xóa
function deleteData(id) {
    $("#delete-IDCategory").val(id);
}

function xoaDanhMuc() {
    let id = $("#delete-IDCategory").val();
    $.ajax({
        type: 'POST',
        data: { "id": id },
        url: '/Category/Delete',
        success: function (response) {
            if (response.status == true) {
                $(".cancelPopup").click();
                $("#row-" + id).remove();
            }
        },
        error: function (response) {
            console.log(xhr.responseText);
            alert("Error has occurred..");
        }
    });
}