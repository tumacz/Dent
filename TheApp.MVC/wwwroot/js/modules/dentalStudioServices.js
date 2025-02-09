const DentalStudioServices = {
    renderServices: (services, container) => {
        container.empty();
        for (const service of services) {
            container.append(`
                <div class="card border-secondary mb-3" style="max-width: 18rem;" data-id="${service.id}">
                    <div class="card-header">
                        ${service.cost}
                        <button class="btn btn-danger btn-sm float-end delete-service" data-id="${service.id}">Delete</button>
                    </div>
                    <div class="card-body">
                        <h5 class="card-title">${service.description}</h5> 
                    </div>
                </div>
            `);
        }

        $(".delete-service").click(function () {
            const id = $(this).data("id");
            DentalStudioServices.deleteService(id);
        });
    },

    loadServices: () => {
        const container = $("#services");
        const dentalStudioEncodedName = container.data("encodedName");

        $.ajax({
            url: `/DentalStudio/${dentalStudioEncodedName}/DentalStudioService`,
            type: 'GET',
            success: function (data) {
                if (!data.length) {
                    container.html("There are no services for this dental studio");
                } else {
                    DentalStudioServices.renderServices(data, container);
                }
            },
            error: function () {
                toastr["error"]("Something went wrong");
            }
        });
    },

    deleteService: (id) => {
        $.ajax({
            url: `/DentalStudio/DentalStudioService/${id}`,
            type: 'DELETE',
            success: function () {
                toastr["success"]("Service deleted successfully");
                DentalStudioServices.loadServices();
            },
            error: function () {
                toastr["error"]("Failed to delete service");
            }
        });
    }
};

$(document).ready(function () {
    DentalStudioServices.loadServices();
});
