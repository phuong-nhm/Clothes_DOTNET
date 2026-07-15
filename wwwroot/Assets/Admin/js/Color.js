function loadDataC(id) {
    console.log("Tên Color: " + JSON.stringify(colorName));
    console.log("Color " + JSON.stringify(id));
    $.ajax({
        type: 'POST',
        url: '/Color/Load',
        contentType: 'application/json',
        data: JSON.stringify({ id: id }),
        success: function (response) {
            $("#changePopup").show();
            $("#editId").val(response.id);
            $("#editName").val(response.colorName);
        },
        error: function () {
            alert('Lỗi xảy ra khi tải dữ liệu.');
        }
    });
}


function themColor() {
    // Lấy dữ liệu từ form
    let data = {};
    let formData = $('#add-form').serializeArray({}); // Lấy dữ liệu từ form
    alert(formData); // Kiểm tra dữ liệu đã được gửi hay chưa
    vxm,xc
    // Duyệt qua formData và tạo đối tượng dữ liệu cho request
    $.each(formData, function (index, value) {
        data["" + value.name + ""] = value.value;
    });

    console.log("Tên Color: " + data.ColorName);
    console.log("Color " + JSON.stringify(data));

    $.ajax({
        url: '/Color/Create',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(data),
        dataType: 'json',
        success: function (response) {
            if (response.status === true) {
                $("#add-message").addClass("text-success").removeClass("text-danger");
                $("#add-message").html(response.message);
                setTimeout(function () {
                    window.location.replace("/Admin/Color");
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

function suaColor() {
    let data = {};
    let formData = $('#update-form').serializeArray({
    });
    $.each(formData, function (index, value) {
        data["" + value.name + ""] = value.value;
    });
    $.ajax({
        url: '/Color/Update',
        type: 'post',
        contentType: 'application/json',
        data: JSON.stringify(data),
        dataType: 'json',
        success: function (respone) {
            $("#update-message").html(respone.message);
            if (respone.status === true) {
                $("#update-message").addClass("text-warning");
                setTimeout(function () {
                    window.location.replace("/Admin/Color");
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

function deleteData(id) {
    $("#delete-id").val(id);
}

function xoaColor() {
    let id = $("#delete-id").val();
    console.log("Id cần xóa: " + id);
    $.ajax({
        type: 'POST',
        data: JSON.stringify({ id: parseInt(id) }),
        url: '/Color/Delete',
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