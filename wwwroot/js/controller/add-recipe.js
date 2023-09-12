var AddRecipe = {
    Init: function () {
        AddRecipe.InitSelect2();
        AddRecipe.RegisterEvent();
    },
    RegisterEvent: function () {

        $("#btnAdd").off('click').on('click', function () {

            var newRecipe = $('.recipe-template').clone();
            newRecipe.removeClass('recipe-template');
            newRecipe.addClass('recipe-item');
            $('.recipe-container').append(newRecipe);
            AddRecipe.InitSelect2();
            AddRecipe.RegisterEvent();
        });

        $('.btn-delete-receip').off('click').on('click', function () {
            if ($('.recipe-item').length > 1) {
                $(this).closest('.recipe-item').remove();
                AddRecipe.RegisterEvent();

            }

        })
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
                    closeButton.innerHTML = 'x';
                    closeButton.onclick = function () {
                        imageContainer.removeChild(image);
                    };

                    const div = document.createElement('div');
                    div.appendChild(image);
                    div.appendChild(closeButton);

                    imageContainer.appendChild(div);
                    AddRecipe.RegisterEvent();
                };

                reader.readAsDataURL(file);
            }
        })

        $('#image-container button').off('click').on('click', function () {
            $(this).parent().remove();
        })

       
        $('.save-recipe-form').off('submit').on('submit', function (event) {
            event.preventDefault();
            AddRecipe.SaveRecipe();
        })
    },
    InitSelect2: function () {
        $('.recipe-item .select2-item').each((i, v) => {
            if ($(v).data('select2')) {
                $(v).select2('destroy');
            };
        })

        $('.recipe-item .select2-item').each((i, v) => {
            $(v).select2();
        })

    },
    SaveRecipe: function () {
 
        if (($('#title').val() === "")
            || ($('#description').val() === "")
            || ($('#instruction').val() === "")) {
            Swal.fire("You must fill all information to add new recipe!", "error")
            return;
        }
        if ($('#image-container img').attr('src') === null || $('#image-container img').attr('src') === undefined) {
            alert("You must have at least an img for this recipe!");
            return;
        }
        $('.recipe-item').each(function (i, v) {
            var quantityValue = $(v).find('.quantity').val();
            var regex = /^\d+\s\w$/;
            if (quantityValue === '') {
                Swal.fire("Please enter the quantity of all your ingredient", "error");
               /* alert("Please enter the quantity of all your ingredient" );*/
                return;
            }

            if (!regex.test(quantityValue)) {
                alert("Please enter a valid quantity for the ingredient and omit the unit.");
                return;
            }
        });

        const data = {
            id: parseInt($("#_id").val()),
            title: $('#title').val(),
            description: $('#description').val(),
            cuisines: $('#cuisines').val(),
            servings: $('#servings').val(),
            cookingTime: $('#cookingTime').val(),
            category: $('#category').val(),
            isFree: $('#isFree').val(),
            instruction: $('#instruction').val()
        };

        //tim image
        var imgArr = [];
        $('#image-container img').each(function (i, v) {
            imgArr.push($(v).attr('src'));
        })
        //Nguyen lieu
        var ingredientArr = [];
        $('.recipe-item').each(function (i, v) {
            var ingredient = {
                quantity: $(v).find('.quantity').val(),
                ingredientId: $(v).find('.select2-item').val()
            }
            ingredientArr.push(ingredient);
        })
        data.imgs = imgArr;
        data.ingredients = ingredientArr;
        //goi ajax submit form
        $.ajax({
            url: 'https://localhost:7034/Recipe/SaveRecipe', 
            type: 'POST',
            data: JSON.stringify(data),
            contentType: 'application/json',
            success: function (response) {
                // Xử lý khi yêu cầu thành công
                console.log('Yêu cầu thành công');
                console.log(response);
                Swal.fire("Hey, Good job !!", "You clicked the button !!", "success").then(function () {
                    window.location = "/admin/RecipesByAdmin";
                });
            },
            error: function (xhr, status, error) {
                console.log('Yêu cầu thất bại');
                console.log(data);
                console.log(error);
            }
        });

    }
}

AddRecipe.Init();