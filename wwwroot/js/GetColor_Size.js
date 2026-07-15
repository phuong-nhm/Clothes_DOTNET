
    function setSelectedSize() {
    const selectedSize = document.querySelector('input[name="size-outlined"]:checked');
    if (selectedSize) {
        const sizeId = selectedSize.value;
    const sizeName = selectedSize.nextElementSibling.innerText; 

    document.getElementById('selectedSizeId').value = sizeId;
    document.getElementById('selectedSizeName').value = sizeName;
    }
}

function setSelectedColor() {
    const selectedColor = document.querySelector('input[name="color-outlined"]:checked');
    if (selectedColor) {
        const colorId = selectedColor.value;
        const colorName = selectedColor.nextElementSibling.innerText;

        document.getElementById('selectedColorId').value = colorId;
        document.getElementById('selectedColorName').value = colorName;
    }
}
function setSelectedOptions() {
    const quantityDisplay = $('#quantityDisplay').text();
    if (quantityDisplay.includes("Hết hàng")) {
        event.preventDefault(); 
        alert("Sản phẩm này hiện không còn hàng. Vui lòng chọn sản phẩm khác!");
    } else {
        

        const count = parseInt($('#Count').val(), 10);
        const quantity = parseInt(quantityDisplay.replace(/\D/g, ''), 10);
        if (count > quantity) {
            event.preventDefault();
            alert("Số lượng sản phẩm đã vượt quá số lượng hiện có vui lòng chọn lại!");
        }
        else {
            setSelectedSize();
            setSelectedColor();
        }

    }


}
function updateQuantityDisplay() {
 
    const selectedSizeId = $('input[name="size-outlined"]:checked').val();
    const selectedColorId = $('input[name="color-outlined"]:checked').val();
    const productId = $('#productId').val();
    if (selectedSizeId && selectedColorId) {
        // Gọi API để lấy số lượng
        $.ajax({
            url: '/Sale/GetProductVariantQuantity',
            type: 'GET',
            data: {
                sizeId: selectedSizeId,
                colorId: selectedColorId,
                productId: productId
            },
            success: function (res) {
                var quantity = res.quantity;
                var id = res.id;
                $('#quantityDisplay').text("Total Quantity: " + quantity);
                if (quantity > 0) {
                    $('#quantityDisplay').text("Total Quantity: " + quantity).removeClass('text-danger').addClass('text-success');
                    document.getElementById("ProductIdVariant").value = id;
                } else {
                    $('#quantityDisplay').text("Hết hàng").removeClass('text-success').addClass('text-danger');
                    document.getElementById("ProductIdVariant").value = ''; // Xóa giá trị nếu hết hàng
                }
                console.log("Product Variant ID:", id);
            },
            error: function (xhr, status, error) {
                console.error('Error fetching quantity:', error);
                $('#quantityDisplay').text("Total Quantity: 0");
            }
        });
    } else {
        $('#quantityDisplay').text("Total Quantity: 0");
    }
}