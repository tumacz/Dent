const UserRolesManager = {
    rolesList: [], // Current roles
    availableRolesList: [], // Available roles

    renderRoles: () => {
        const rolesContainer = document.getElementById('roles-container');
        const availableRolesContainer = document.getElementById('available-roles-container');

        // Clear both containers
        rolesContainer.innerHTML = "";
        availableRolesContainer.innerHTML = "";

        // Render current roles
        UserRolesManager.rolesList.forEach((role) => {
            const roleEntry = document.createElement('span');
            roleEntry.className = 'role-tab d-inline-flex align-items-center bg-danger text-white rounded px-3 py-2 me-2 mb-2';
            roleEntry.dataset.role = role;

            roleEntry.innerHTML = `
                <span>${role}</span>
                <button type="button" class="btn-close btn-close-white ms-2" aria-label="Remove"></button>
            `;

            roleEntry.querySelector('.btn-close').addEventListener('click', () => {
                UserRolesManager.removeRole(role);
            });

            rolesContainer.appendChild(roleEntry);
        });

        // Render available roles
        UserRolesManager.availableRolesList
            .filter(role => !UserRolesManager.rolesList.includes(role)) // Exclude roles already assigned
            .forEach((role) => {
                const roleEntry = document.createElement('span');
                roleEntry.className = 'role-tab d-inline-flex align-items-center bg-primary text-white rounded px-3 py-2 me-2 mb-2';
                roleEntry.dataset.role = role;

                roleEntry.innerHTML = `
                    <span>${role}</span>
                <button type="button" class="btn-close btn-close-white ms-2" aria-label="Add"></button>
                `;

                roleEntry.addEventListener('click', () => {
                    UserRolesManager.addRole(role);
                });

                availableRolesContainer.appendChild(roleEntry);
            });

        // Update the hidden input field with the current roles list
        UserRolesManager.updateHiddenRolesField();
    },

    updateHiddenRolesField: () => {
        // Updates the hidden input field with the serialized roles list to send in the form submission.
        const rolesInput = document.querySelector('input[name="Roles"]');
        rolesInput.value = JSON.stringify(UserRolesManager.rolesList);
    },

    addRole: (role) => {
        // Adds a role to the roles list if valid, and re-renders the roles.
        if (role && !UserRolesManager.rolesList.includes(role)) {
            UserRolesManager.rolesList.push(role);
            UserRolesManager.renderRoles();
        }
    },

    removeRole: (role) => {
        // Removes a role from the roles list and re-renders the roles.
        const roleIndex = UserRolesManager.rolesList.indexOf(role);
        if (roleIndex > -1) {
            UserRolesManager.rolesList.splice(roleIndex, 1);
            UserRolesManager.renderRoles();
        }
    },

    fillEditModal: (userId, userName, userEmail, userRoles, availableRoles) => {
        // Fills the edit modal fields with the provided user data and initializes the roles and available roles lists.
        const idField = document.querySelector('#EditUserRolesModal input[name="Id"]');
        const userNameField = document.querySelector('#EditUserRolesModal input[name="UserName"]');
        const emailField = document.querySelector('#EditUserRolesModal input[name="Email"]');

        idField.value = userId;
        userNameField.value = userName;
        emailField.value = userEmail;

        UserRolesManager.rolesList = [...userRoles];
        UserRolesManager.availableRolesList = [...availableRoles];
        UserRolesManager.renderRoles();
    },

    initEditButtons: () => {
        // Initializes event listeners for edit buttons.
        const editButtons = document.querySelectorAll('.edit-button');
        editButtons.forEach(button => {
            button.addEventListener('click', () => {
                const userId = button.dataset.userId;
                const userName = button.dataset.userName;
                const userEmail = button.dataset.userEmail;
                const userRoles = JSON.parse(button.dataset.userRoles);
                const availableRoles = JSON.parse(button.dataset.availableRoles);

                UserRolesManager.fillEditModal(userId, userName, userEmail, userRoles, availableRoles);
            });
        });
    }
};

// Main initialization
document.addEventListener("DOMContentLoaded", () => {
    UserRolesManager.initEditButtons();

    const form = document.querySelector('#EditUserRolesForm');
    if (form) {
        form.addEventListener('submit', () => {
            UserRolesManager.updateHiddenRolesField();
        });
    }
});
