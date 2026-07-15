function loadData(id) {
    console.log("Tên danh mục: " );
    console.log("Danh muc ", id);
    $.ajax({
        type: 'POST',
        url: '/Roles/Load',
        contentType: 'application/json',
        data: JSON.stringify({ id: id }), // gửi như form data
        success: function (response) {
            console.log(response);
            $("#changePopUp").show();
            $("#role-id").val(response.idrl);  // Đặt đúng tên input Id
            $("#role-name").val(response.name);  // Đặt đúng tên input
            $("#roleNN").val(response.nn);
            $("#roleCS").val(response.cs);
        },
        error: function(xhr, status, error) {
            console.log("Lỗi:", error);
            console.log("Trạng thái:", status);
            console.log("Phản hồi:", xhr.responseText);
            alert("Có lỗi xảy ra khi gửi dữ liệu AJAX");
        }
    });
}
// Hàm xử lý thay đổi vai trò
function changeRole() {
    // Lấy dữ liệu từ form
    const roleId = $("#role-id").val();
    const roleName = $("#role-name").val();
    const roleNN = $("#roleNN").val();
    const roleCS = $("#roleCS").val();

    // Kiểm tra dữ liệu
    if (!roleId || !roleName || !roleNN || !roleCS) {
        alert("Vui lòng điền đầy đủ thông tin.");
        return;
    }

    // Gửi dữ liệu qua AJAX
    $.ajax({
        type: 'POST',
        url: '/Roles/Update', // Thay URL với đường dẫn xử lý cập nhật trên server
        contentType: 'application/json',
        data: JSON.stringify({
            id: roleId,
            name: roleName,
            normalizedName: roleNN,
            concurrencyStamp: roleCS
        }),
        success: function(response) {
            console.log("Cập nhật thành công:", response);
            if (response.status === true) {
                swal({
                    title: "Thành công!",
                    text: "Sửa Role thành công!",
                    icon: "success",
                    button: false
                }).then(() => {
                    window.location.replace("/Admin/Roles"); // Tự động load lại trang
                });;
            } else {
                alert("Cập nhật thất bại: " + response.message);
            }
        },
        error: function(xhr, status, error) {
            console.log("Lỗi:", error);
            console.log("Trạng thái:", status);
            console.log("Phản hồi:", xhr.responseText);
            alert("Có lỗi xảy ra khi cập nhật vai trò.");
        }
    });
}

function themRoles() {
    // Lấy dữ liệu từ form
    const roleId = $("#idr").val();
    const roleName = $("#namer").val();
    const roleNN = $("#rolenn").val();  // NormalizedName
    const roleCS = $("#rolecs").val();  // ConcurrencyStamp

    // Gửi dữ liệu qua AJAX
    $.ajax({
        type: 'POST',
        url: '/Roles/Create', // Thay URL với đường dẫn xử lý tạo role trên server
        contentType: 'application/json',
        data: JSON.stringify({
            id: roleId,
            name: roleName,
            normalizedName: roleNN,  // Thêm NormalizedName
            concurrencyStamp: roleCS  // Thêm ConcurrencyStamp
        }),
        success: function(response) {
            if (response.status === true) {
                swal({
                    title: "Thành công!",
                    text: "Thêm Role thành công!",
                    icon: "success",
                    button: false
                }).then(() => {
                    window.location.replace("/Admin/Roles"); // Tự động load lại trang
                });;
            } else {
                alert(response.message);
            }
        },
        error: function(xhr, status, error) {
            console.log("Lỗi:", error);
            console.log("Trạng thái:", status);
            console.log("Phản hồi:", xhr.responseText);
            alert("Có lỗi xảy ra khi cập nhật vai trò.");
        }
    });
}
function deleteData(id) {
    $("#delete-id").val(id);
}
function xoaRoles() {
    // Lấy ID role từ input hidden
    const roleId = $("#delete-id").val();
    alert("ID:   "+roleId);
    // Gửi yêu cầu xóa role qua AJAX
    $.ajax({
        type: 'POST',
        url: '/Roles/Delete',  // Đường dẫn tới controller xử lý xóa role
        contentType: 'application/json',
        data: JSON.stringify({ id: roleId }),  // Gửi id của role cần xóa
        success: function(response) {
            if (response.status === true) {
                alert("Xóa role thành công!");
                // Ẩn popup sau khi xóa thành công
                $("#deleteModal").modal('hide');  // Giả sử bạn dùng modal để hiển thị popup
                // Thực hiện cập nhật giao diện, ví dụ là xóa dòng role trong bảng
                $("#role-row-" + roleId).remove();  // Xóa dòng role tương ứng trong bảng
                window.location.replace("/Admin/Roles");
            } else {
                alert("Xóa role thất bại: " + response.message);
            }
        },
        error: function(xhr, status, error) {
            console.log("Lỗi:", error);
            console.log("Trạng thái:", status);
            console.log("Phản hồi:", xhr.responseText);
            alert("Có lỗi xảy ra khi cập nhật vai trò.");
        }
    });
}

