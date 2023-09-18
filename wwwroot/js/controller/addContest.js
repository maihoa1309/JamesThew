var addContest = {
    Init: function () {
        addContest.RegisterEvent();
    },
    RegisterEvent: function () {
        $(".saveContest").off('submit').on('submit', function (event) {
            event.preventDefault();
            addContest.SaveContest();
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
            EndDate: moment($('#endDate').val(), 'DD MMMM, YYYY')
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