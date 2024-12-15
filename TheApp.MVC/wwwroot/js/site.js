const RenderDentalStudioServices = (services, container) => {
    container.empty();
    for (const service of services) {
        container.append(
            `<div class="card border-secondary mb-3" style="max-width: 18rem;">
          <div class="card-header">${service.cost}</div>
          <div class="card-body">
            <h5 class="card-title">${service.description}</h5> 
          </div>
        </div>`)
    }
}
const LoadDentalStudioServices = () => {
    const container = $("#services")
    const dentalStudioEncodedName = container.data("encodedName");
    $.ajax({
        url: `/DentalStudio/${dentalStudioEncodedName}/DentalStudioService`,
        type: 'get',
        success: function (data) {
            if (!data.length) {
                container.html("There are no services for this dental studio")
            } else {
                RenderDentalStudioServices(data, container)
            }
        },
        error: function () {
            toastr["error"]("Something went wrong")
        }
    })
}

document.addEventListener("DOMContentLoaded", () => {
    const userCards = document.querySelectorAll(".user-card");

    userCards.forEach(card => {
        const rolesContainer = card.querySelector(".roles-list");
        const rolesData = card.dataset.roles;

        if (rolesData) {
            const roles = JSON.parse(rolesData);
            roles.forEach(role => {

                const roleElement = document.createElement("div");
                roleElement.className = "role-item";
                roleElement.textContent = role;
                rolesContainer.appendChild(roleElement);
            });
        } else {
            rolesContainer.textContent = "No roles assigned.";
        }
    });
});