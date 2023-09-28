var updateUser = {
    Init: function () {
        updateUser.RegisterEvent();
    },
    RegisterEvent: function () {
        $('#submit').on('click', function () {
            event.preventDefault();
            updateUser.SaveUser();
        })

    },
    SaveUser: function () {
        if (($('#name').val() === "") || ($('#gender').val() === "") || ($('#age').val() === "")) {
            Swal.fire("Oops...", "You must fill all information to save user!", "error");
            return;
        }

        const data = {
            Id: $("#_id").val(),
            Gender: $('#gender').val(),
            Name: $('#name').val(),
            Age: parseInt($('#age').val()),
            Avatar: $('#image-container img').attr('src'),
            RoleId: $('#roleId').val(),
            IsDeleted: false
        };
        console.log(data)
        $.ajax({
            url: 'https://localhost:7034/CustomUser/SaveUser',
            type: 'POST',
            data: JSON.stringify(data),
            contentType: 'application/json',
            success: function (response) {
                console.log("yeah");
            },
            error: function (xhr, status, error) {
                console.log(xhr);
                console.log(error);
            }
        });
        updateUser.RegisterEvent();
    }
}

updateUser.Init();