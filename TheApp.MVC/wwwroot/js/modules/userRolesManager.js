const UserRolesManager = {
    rolesList: [],

    renderRoles: () => {
        // Renders the roles in the roles container by creating input fields and remove buttons for each role.
        const rolesContainer = document.getElementById('roles-container');
        rolesContainer.innerHTML = "";

        UserRolesManager.rolesList.forEach((role, index) => {
            const roleEntry = document.createElement('div');
            roleEntry.className = 'input-group mb-2 role-entry';

            roleEntry.innerHTML = `
                <input name="Roles[${index}]" class="form-control" value="${role}" />
                <button type="button" class="btn btn-danger btn-remove-role">Remove</button>
            `;

            rolesContainer.appendChild(roleEntry);
        });
    },

    updateHiddenRolesField: () => {
        // Updates the hidden input field with the serialized roles list to send in the form submission.
        const roles = document.querySelectorAll('.role-entry input');
        const rolesArray = Array.from(roles).map(role => role.value);
        const rolesInput = document.querySelector('input[name="Roles"]');
        rolesInput.value = JSON.stringify(rolesArray);
    },

    removeRole: (index) => {
        // Removes a role from the roles list and re-renders the roles.
        UserRolesManager.rolesList.splice(index, 1);
        UserRolesManager.renderRoles();
    },

    addRole: () => {
        // Prompts the user for a new role name, adds it to the roles list if valid, and re-renders the roles.
        const newRole = prompt("Enter new role name:");
        if (newRole && !UserRolesManager.rolesList.includes(newRole)) {
            UserRolesManager.rolesList.push(newRole);
            UserRolesManager.renderRoles();
        } else {
            alert("Role is invalid or already exists!");
        }
    },

    loadRoles: () => {
        // Loads a predefined list of roles into the roles container.
        const rolesContainer = document.getElementById('roles-container');
        if (!rolesContainer) {
            console.error("Roles container not found!");
            return;
        }

        rolesContainer.innerHTML = "";

        const roles = ["test"];

        roles.forEach((role, index) => {
            const roleEntry = document.createElement('div');
            roleEntry.className = 'input-group mb-2 role-entry';

            roleEntry.innerHTML = `
            <input name="Roles[${index}]" class="form-control" value="${role}" />
            <button type="button" class="btn btn-danger btn-remove-role">Remove</button>
        `;

            rolesContainer.appendChild(roleEntry);
        });
    },     

    fillEditModal: (userId, userName, userEmail, userRoles) => {
        // Fills the edit modal fields with the provided user data and initializes the roles list.
        const idField = document.querySelector('#EditUserRolesModal input[name="Id"]');
        const userNameField = document.querySelector('#EditUserRolesModal input[name="UserName"]');
        const emailField = document.querySelector('#EditUserRolesModal input[name="Email"]');

        idField.value = userId;
        userNameField.value = userName;
        emailField.value = userEmail;

        UserRolesManager.rolesList = [...userRoles];
        UserRolesManager.renderRoles();
    },

    initEditButtons: () => {
        // Initializes event listeners for edit buttons and the "add role" button.
        const editButtons = document.querySelectorAll('.edit-button');
        editButtons.forEach(button => {
            button.addEventListener('click', () => {
                // Handles the click event for edit buttons by filling the edit modal with user data.
                const userId = button.dataset.userId;
                const userName = button.dataset.userName;
                const userEmail = button.dataset.userEmail;
                const userRoles = JSON.parse(button.dataset.userRoles);

                UserRolesManager.fillEditModal(userId, userName, userEmail, userRoles);
            });
        });

        const addRoleButton = document.getElementById('add-role-button');
        if (addRoleButton) {
            // Handles the click event for the "add role" button.
            addRoleButton.addEventListener('click', UserRolesManager.addRole);
        }
    }
};

// main
document.addEventListener("DOMContentLoaded", () => {
    // Initializes the application once the DOM content is fully loaded.
    UserRolesManager.initEditButtons();

    const form = document.querySelector('#EditUserRolesForm');
    if (form) {
        // Updates the hidden roles field before form submission.
        form.addEventListener('submit', (event) => {
            UserRolesManager.updateHiddenRolesField();
            console.log("Roles sent:", document.querySelector('input[name="Roles"]').value);
        });
    }

    document.getElementById('roles-container').addEventListener('click', function (event) {
        // Handles click events within the roles container, such as removing a role.
        if (event.target.classList.contains('btn-remove-role')) {
            const entry = event.target.closest('.role-entry');
            entry.remove();

            const roles = document.querySelectorAll('.role-entry input');
            roles.forEach((role, index) => {
                role.name = `Roles[${index}]`;
            });

            UserRolesManager.updateHiddenRolesField();
        }
    });
});
