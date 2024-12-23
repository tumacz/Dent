document.addEventListener("DOMContentLoaded", () => {
    if ($("#services").length) {
        DentalStudioServices.loadServices();
    }

    if (document.querySelectorAll(".user-card").length > 0) {
        UserRolesManager.init();
    }
});