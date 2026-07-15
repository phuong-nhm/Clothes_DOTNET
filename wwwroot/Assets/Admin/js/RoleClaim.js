function loadData(id) {
    console.log("Tên danh mục: " );
    console.log("Danh muc ", id);
    $.ajax({
        type: 'POST',
        url: '/RoleClaim/Load',
        contentType: 'application/json',
        data: JSON.stringify({ id: id }), // gửi như form data
        success: function (response) {
            console.log("Response từ server:", response);

            // Hiển thị popup
            $("#changePopUp").show();
            setTimeout(function () {
                $("#idRoleCL1").val(response.idrl)
                $("#idRole1").val(response.rlid)  
                $("#cltype1").val(response.cltp)
                $("#clvalue1").val(response.clvl)
            }, 100); // Trì hoãn 100ms
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
function changeRoleClaim() {
    // Lấy dữ liệu từ form
    const clid = $("#idRoleCL1").val();
    const roleId = $("#idRole1").val();
    const claimtype = $("#cltype1").val();
    const claimvalue = $("#clvalue1").val();

    // Kiểm tra dữ liệu
    if (!clid || !roleId || !claimtype || !claimvalue) {
        alert("Vui lòng điền đầy đủ thông tin.");
        return;
    }

    // Gửi dữ liệu qua AJAX
    $.ajax({
        type: 'POST',
        url: '/RoleClaim/Update', // Thay URL với đường dẫn xử lý cập nhật trên server
        contentType: 'application/json',
        data: JSON.stringify({
            id: clid,
            idrl: roleId,
            cltp: claimtype,
            clvl: claimvalue
        }),
        success: function(response) {
            console.log("Cập nhật thành công:", response);
            if (response.status === true) {
                alert("hahaahahahhaha" +response)
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

function themRoleClaim() {
    // Lấy dữ liệu từ form
    const roleId = $("#idRole").val();
    const claimtype = $("#cltype").val();
    const claimvalue = $("#clvalue").val();
    // Kiểm tra dữ liệu trước khi gửi
    if (!roleId || !claimtype || !claimvalue) {
        alert("Vui lòng nhập đầy đủ thông tin!");
        return;
    }
    console.log("thông tin:", roleId);

    // Gửi dữ liệu qua AJAX
    $.ajax({
        type: 'POST',
        url: '/RoleClaim/Create', // Thay URL với đường dẫn xử lý tạo role trên server
        contentType: 'application/json',
        data: JSON.stringify({
            idrl: roleId,
            cltp: claimtype,
            clvl: claimvalue
        }),
        success: function(response) {
            if (response.status === true) {
                swal({
                    title: "Thành công!",
                    text: "Thêm Role thành công!",
                    icon: "success",
                    button: false
                })
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
function xoaRoleClaim() {
    // Lấy ID role từ input hidden
    const clId = $("#delete-id").val();
    alert("ID:   "+clId);
    // Gửi yêu cầu xóa role qua AJAX
    $.ajax({
        type: 'POST',
        url: '/RoleClaim/Delete',  // Đường dẫn tới controller xử lý xóa role
        contentType: 'application/json',
        data: JSON.stringify({ id: clId }),  // Gửi id của role cần xóa
        success: function(response) {
            if (response.status === true) {
                alert("Xóa role thành công!");
                // Ẩn popup sau khi xóa thành công
                $("#deleteModal").modal('hide');  // Giả sử bạn dùng modal để hiển thị popup
                // Thực hiện cập nhật giao diện, ví dụ là xóa dòng role trong bảng
                $("#role-row-" + clId).remove();  // Xóa dòng role tương ứng trong bảng
                window.location.replace("/Admin/RoleClaim");
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

