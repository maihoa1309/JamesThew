﻿var addCategory = {
    Init: function () {
        addCategory.RegisterEvent();
    },
    RegisterEvent: function () {
        $(".saveCategory").off('submit').on('submit', function (event) {
            event.preventDefault();
            addCategory.SaveContest();
        }),
        $('#formFile').off('change').on('change', function () {
            const files = $(this)[0].files;
            const imageContainer = document.getElementById('image-container');

            // Xóa các ảnh hiện có trong imageContainer (nếu có)
            imageContainer.innerHTML = '';

            if (files.length > 0) {
                const file = files[0];
                const reader = new FileReader();

                reader.onload = function (e) {
                    const image = document.createElement('img');
                    image.src = e.target.result;

                    const closeButton = document.createElement('button');
                    closeButton.setAttribute('type', 'button');
                    closeButton.setAttribute('class', 'btn btn-danger');
                    closeButton.innerHTML = '<i class="fas fa-times"></i>'
                    closeButton.onclick = function () {
                        div.remove();
                    };

                    const div = document.createElement('div');
                    div.appendChild(image);
                    div.appendChild(closeButton);

                    imageContainer.appendChild(div);
                    updateUser.RegisterEvent();
                };

                reader.readAsDataURL(file);
            }
        });

        $('#image-container button').off('click').on('click', function () {
            $(this).parent().remove();
            addCategory.RegisterEvent();
        })

    },
    SaveContest: function () {
        if (($('#name').val() === "")) {
            Swal.fire("Oops...", "You must fill all information to save category!", "error");
            return;
        }

        const data = {
            Id: parseInt($("#_id").val()),
            Name: $('#name').val(),
            Img: $('#image-container img').attr('src')
        };
        $.ajax({
            url: 'https://localhost:7034/Category/SaveCategory',
            type: 'POST',
            data: JSON.stringify(data),
            contentType: 'application/json',
            success: function (response) {
                console.log(data);
            },
            error: function (xhr, status, error) {
                console.log(xhr);
                console.log(error);
            }
        });
        addCategory.RegisterEvent();
    }
}

addCategory.Init();