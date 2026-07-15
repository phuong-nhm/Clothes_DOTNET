/*function loadData(id) {
    $.ajax({
        type: 'POST',
        url: '/CategoryClaim',     
        contentType: 'application/json', 
        data: JSON.stringify({ id: id }),
        dataType: 'json', 
        success: function (response) {
            $("#id").val(response.Id);
            $("#claimname").val(response.ClaimName);
        },
        error: function (response) {
            //debugger;  
            console.log(response.responseText);
            alert("Error has occurred..");
        }
    });
}*/

function loadData(id) {
    console.log("Tên danh mục: " ); // Kiểm tra giá trị của NameCategory
    console.log("Danh muc " + JSON.stringify(id));
    $.ajax({
        type: 'POST',
        url: '/CategoryClaim/Load',
        contentType: 'application/json',
        data: JSON.stringify({ id: id }),
        success: function (response) {
           /* if (!response || response.status === false) {
                alert(response.message || 'Không thể tải dữ liệu.');
                return;
            }*/
            // Gán dữ liệu vào form chỉnh sửa
            $("#changePopUp").show();
            $("#editId").val(response.id);  // Đặt đúng tên input Id
            $("#editClaimName").val(response.claimName);  // Đặt đúng tên input ClaimName
        },
        error: function () {
            alert('Lỗi xảy ra khi tải dữ liệu.');
        }
    });
}

//gửi ajax thêm danh mục
function themPhanMuc() {
    // Lấy dữ liệu từ form
    let data = {};
    let formData = $('#add-form').serializeArray({}); // Lấy dữ liệu từ form
    console.log(formData); // Kiểm tra dữ liệu đã được gửi hay chưa

    // Duyệt qua formData và tạo đối tượng dữ liệu cho request
    $.each(formData, function (index, value) {
        data["" + value.name + ""] = value.value;
    });

    // Kiểm tra giá trị của NameCategory
    console.log("Tên danh mục: " + data.NameClaim); // Kiểm tra giá trị của NameCategory
    console.log("Danh muc " + JSON.stringify(data));
    // Gửi request đến server
    $.ajax({
        url: '/CategoryClaim/Create',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(data),
        dataType: 'json',
        success: function (response) {
            if (response.status === true) {
                $("#add-message").addClass("text-success").removeClass("text-danger");
                $("#add-message").html(response.message);
                setTimeout(function () {
                    window.location.replace("/Admin/CategoryClaim");
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

function suaPhanMuc() {
    let data = {};
    let formData = $('#update-form').serializeArray({
    });
    $.each(formData, function (index, value) {
        data["" + value.name + ""] = value.value;
    });
    $.ajax({
        url: '/CategoryClaim/Update',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(data),
        dataType: 'json',
        success: function (respone) {
            $("#update-message").html(respone.message);
            if (respone.status === true) {
                $("#update-message").addClass("text-warning");
                setTimeout(function () {
                    window.location.replace("/Admin/CategoryClaim");
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
    $("#delete-id").val(id);
}

function xoaPhanMuc() {
    let id = $("#delete-id").val();
    console.log("Id cần xóa: " + id);
    $.ajax({
        type: 'POST',
        data: JSON.stringify({ id: parseInt(id) }) ,
        url: '/CategoryClaim/Delete',
        contentType: 'application/json',
        dataType: 'json',
        success: function (response) {
            if (response.status === true) {
                $(".cancelPopup").click();
                $("#row-" + id).remove();
            }
        },
        error: function (response) {
            console.log(response);
            alert("Error has occurred..");
        }
    });
}