function convertToInput(divElement) {
    // Tạo một thẻ input
    var inputElement = document.createElement("input");

    // Sao chép nội dung từ div vào input
    inputElement.value = divElement.textContent;

    // Thay thế div bằng input
    divElement.parentNode.replaceChild(inputElement, divElement);

    // Tự đặt trỏ chuột vào input và chọn toàn bộ nội dung
    inputElement.focus();
    inputElement.select();

    // Thêm sự kiện lắng nghe cho input để chuyển ngược lại div khi mất focus
    inputElement.addEventListener("blur", function () {
        // Thay thế input bằng div với nội dung mới
        var newDiv = document.createElement("div");
        newDiv.className = "input-setting";
        newDiv.textContent = inputElement.value;
        inputElement.parentNode.replaceChild(newDiv, inputElement);
    });
}