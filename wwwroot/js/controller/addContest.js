var addContest = {
    Init: function () {
        addContest.RegisterEvent();
    },
    RegisterEvent: function () {
        $(".saveContest").off('submit').on('submit', function (event) {
            event.preventDefault();
            addContest.SaveContest();
        }),
        $('#formFile').off('change').on('change', function () {
            const files = $(this)[0].files;
            const imageContainer = document.getElementById('image-container');

            for (let i = 0; i < files.length; i++) {
                const file = files[i];
                const reader = new FileReader();

                reader.onload = function (e) {
                    const image = document.createElement('img');
                    image.src = e.target.result;

                    const closeButton = document.createElement('button');
                    closeButton.setAttribute('type', 'button');
                    closeButton.setAttribute('class', 'btn btn-danger');
                    closeButton.innerHTML = '<i class="fas fa-times"></i>'
                    closeButton.onclick = function () {
                        imageContainer.remove(image);
                    };

                    const div = document.createElement('div');
                    div.appendChild(image);
                    div.appendChild(closeButton);

                    imageContainer.appendChild(div);
                    addContest.RegisterEvent();
                };

                reader.readAsDataURL(file);
            }
        })

        $('#deleteImg').off('click').on('click', function () {
            $(this).parent().remove();
            addContest.RegisterEvent();
        })

    },
    SaveContest: function () {
        if (($('#title').val() === "")
            || ($('#description').val() === "")) {
            Swal.fire("Oops...", "You must fill all information to add new contest!", "error");
            return;
        }
        if ((moment($('#endDate').val(), 'DD MMMM, YYYY')).isBefore(moment($('#startDate').val(), 'DD MMMM, YYYY'))) {
            Swal.fire("Oops...", "End date must after start date!", "error");
            return;
        } 
        const data = {
            Id: parseInt($("#_id").val()),
            Title: $('#title').val(),
            Description: $('#description').val(),
            CategoryId: parseInt($('#single-select').val()),
            StartDate: moment($('#startDate').val(), 'DD MMMM, YYYY'),
            EndDate: moment($('#endDate').val(), 'DD MMMM, YYYY'),
            Img : $('#image-container img').attr('src')
        };
        $.ajax({
            url: 'https://localhost:7034/Contest/SaveContest',
            type: 'POST',
            data: JSON.stringify(data),
            contentType: 'application/json',
            success: function (response) {
                console.log(data);
                console.log("yeah");
            },
            error: function (xhr, status, error) {
                console.log(xhr);
                console.log(error);
            }
        });
        addContest.RegisterEvent();
    }
}

addContest.Init();