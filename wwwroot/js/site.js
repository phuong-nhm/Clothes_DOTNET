// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Dữ liệu thay đổi theo thời gian
const messages = [
    "Chào mừng bạn đến với cửa hàng của chúng tôi!",
    "Đừng bỏ lỡ các ưu đãi đặc biệt hôm nay!",
    "Mua ngay để nhận nhiều khuyến mãi hấp dẫn!",
    "Chúng tôi có những sản phẩm mới mỗi ngày!"
];

// Chức năng thay đổi văn bản sau mỗi 3 giây
let currentMessageIndex = 0;
const textElement = document.getElementById('changingText');

function changeText() {
    textElement.textContent = messages[currentMessageIndex];
    currentMessageIndex = (currentMessageIndex + 1) % messages.length;
}
// Thay đổi văn bản mỗi 3 giây
setInterval(changeText, 3000);
