$(document).ready(function () {

    LoadDentalStudioServices()

    $("#createDentalStudioServiceModal form").submit(function (event) {
        event.preventDefault();
        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: $(this).serialize(),
            success: function (data) {
                toastr["success"]("Created dental studio service")
                LoadDentalStudioServices()
            },
            error: function () {
                toastr["error"]("Something went wrong")
            }
        })
    });
});