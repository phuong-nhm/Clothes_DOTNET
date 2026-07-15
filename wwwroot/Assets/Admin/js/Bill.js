function loadData(IDOrder) {
    console.log("ID gửi từ client:", JSON.stringify(IDOrder));  // Kiểm tra ID gửi đi
    $.ajax({
        type: 'POST',
        url: '/Bill/Load',
        contentType: 'application/json',
        data: JSON.stringify({ IDOrder : IDOrder }), // Gửi IDOrder
        success: function(response) {
            console.log("ID Order:", response.idOrder);
            console.log("Response:", response.fullName);
            $("#popUp").show();
            $("#hd-idnguoidat").val(response.idOrder);
            $("#hd-nguoidat").val(response.fullName);
            $("#hd-email").val(response.email);
            $("#hd-trangthai").val(response.status == 0 ? "Đã hủy" :
                (response.status == 1) ? "Đang chuẩn bị" :
                    (response.status == 2) ? "Đang giao" : "Đã thanh toán");
            // $("#hd-ngaydat").val(dateFormat(new Date(parseInt((response.ngaydat).match(/\d+/)[0]))));
            $("#hd-sdt").val(response.sdt);
            $("#hd-diachi").val(response.address);
            $("#hd-ghichu").html(response.note);

            // Xử lý danh sách sản phẩm
            let productList = '';
            response.product.forEach(function (product) {
                    productList += `
                        <tr>
                            <td><img src="${product.image}"></td>
                            <td>${product.pdn}</td>
                            <td>${product.gia ? product.gia.toLocaleString() : '0'} VNĐ</td>
                            <td>${product.soluong} VND</td>
                            <td>${product.tongtien ? product.tongtien.toLocaleString() : '0'} VNĐ</td>
                        </tr>`;
            });
            console.log("chi tiết :",productList);
            
            $("#hd-body").html(productList);
        },
        error: function(xhr, status, error) {
            console.log("Lỗi:", error);
            console.log("Trạng thái:", status);
            console.log("Phản hồi:", xhr.responseText);
            alert("Có lỗi xảy ra khi gửi dữ liệu AJAX");
        }
    });
}
function huyDonHang(id) {
    swal({
        title: "Cảnh báo",
        text: "Bạn có chắc về việc hủy đơn hàng này!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    type: 'POST',
                    data: JSON.stringify({ id : id}),
                    url: '/Bill/ChangeStatus',
                    success: function (response) {
                        if (response.status == true) {
                            swal({
                                title: "Thành công!",
                                text: "Hủy đơn hàng thành công !",
                                icon: "success",
                                timer: 1500,
                                button: false
                            });
                        } else {
                            swal({
                                title: "Thất bại!",
                                text: "Hủy đơn hàng không thành công",
                                icon: "error",
                                timer: 1500,
                                button: false
                            });
                        }
                        setTimeout(function () {
                            window.location = "/Admin/Bill";
                        }, 1500);
                    },
                    error: function(xhr, status, error) {
                        console.log("Lỗi:", error);
                        console.log("Trạng thái:", status);
                        console.log("Phản hồi:", xhr.responseText);
                        alert("Có lỗi xảy ra khi gửi dữ liệu AJAX");
                    }
                });
            }
        });
}

//function doiTrangThai(Statusss, IDOrder) {
//    const Status = Statusss.value;
//    const ido = IDOrder;
//    // let tt = $("#hd-trangthai-update-" + id).val();
//    alert("sta tú :"+ Status + ido)
//    $.ajax({
//        url: '/Bill/ChangeStatus',
//        method: 'POST',
//        data: { Status : Status , ido:ido}, // Gửi JSON
//        contentType: 'application/json',
//        success: function (response) {
//            alert("cặc");
//            alert("sta tú :"+ Status + ido)
//            console.log(response);
//            if (response.status == true) {
//                swal({
//                    title: "Thành công!",
//                    text: "Sửa trạng thái thành công !",
//                    icon: "success",
//                    timer: 1500,
//                    button: false
//                });
//            } else {
//                swal({
//                    title: "Thất bại!",
//                    text: "Xem chi tiết tại giỏ hàng nhé <3",
//                    icon: "error",
//                    timer: 1500,
//                    button: false
//                });
//            }
//        },
//        error: function(xhr, status, error) {
//            console.log("Lỗi:", error);
//            console.log("Trạng thái:", status);
//            console.log("Phản hồi:", xhr.responseText);
//            alert("Có lỗi xảy ra khi gửi dữ liệu AJAX");
//        }
//    });


//}
function doiTrangThai(Statusss, IDOrder) {
    const Status = Statusss.value; // Lấy giá trị trạng thái
    const ido = IDOrder; // Lấy ID đơn hàng

    alert("Trạng thái: " + Status + " | ID đơn hàng: " + ido); // Để kiểm tra thông tin

    $.ajax({
        url: '/Bill/ChangeStatus', // Địa chỉ URL đến controller
        method: 'POST',
        data: { Status: Status, ido: ido }, // Gửi dữ liệu
        success: function (response) {
            // Thông báo nếu cập nhật thành công
            if (response.status == true) {
                swal({
                    title: "Thành công!",
                    text: "Sửa trạng thái thành công!",
                    icon: "success",
                    timer: 1500,
                    button: false
                });
            } else {
                swal({
                    title: "Thất bại!",
                    text: "Có lỗi xảy ra khi cập nhật trạng thái.",
                    icon: "error",
                    timer: 1500,
                    button: false
                });
            }
        },
        error: function (xhr, status, error) {
            // Thông báo nếu có lỗi
            console.log("Lỗi:", error);
            console.log("Trạng thái:", status);
            console.log("Phản hồi:", xhr.responseText);
            alert("Có lỗi xảy ra khi gửi dữ liệu AJAX");
        }
    });
}