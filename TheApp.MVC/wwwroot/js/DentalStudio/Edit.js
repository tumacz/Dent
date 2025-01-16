$(document).ready(function () {
    DentalStudioServices.loadServices();

    $("#createDentalStudioServiceModal form").submit(function (event) {
        event.preventDefault();
        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: $(this).serialize(),
            success: function (data) {
                toastr["success"]("Created dental studio service");
                DentalStudioServices.loadServices();
            },
            error: function () {
                toastr["error"]("Something went wrong");
            }
        });
    });
});
