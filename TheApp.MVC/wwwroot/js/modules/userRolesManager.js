const UserRolesManager = {
    rolesList: [],

    fillEditModal: (userId, userName, userEmail, userRoles) => {
        console.log("Filling modal with data:");
        console.log("User ID:", userId);
        console.log("User Name:", userName);
        console.log("User Email:", userEmail);
        console.log("User Roles:", userRoles);

        const idField = document.querySelector('#EditUserRolesModal input[name="Id"]');
        const userNameField = document.querySelector('#EditUserRolesModal input[name="UserName"]');
        const emailField = document.querySelector('#EditUserRolesModal input[name="Email"]');

        idField.value = userId;
        userNameField.value = userName;
        emailField.value = userEmail;

        UserRolesManager.rolesList = [...userRoles];
        UserRolesManager.renderRoles();
    },

    renderRoles: () => {
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

    removeRole: (index) => {
        UserRolesManager.rolesList.splice(index, 1);
        UserRolesManager.renderRoles();
    },

    addRole: () => {
        const newRole = prompt("Enter new role name:");
        if (newRole && !UserRolesManager.rolesList.includes(newRole)) {
            UserRolesManager.rolesList.push(newRole);
            UserRolesManager.renderRoles();
        } else {
            alert("Role is invalid or already exists!");
        }
    },

    initEditButtons: () => {
        const editButtons = document.querySelectorAll('.edit-button');
        editButtons.forEach(button => {
            button.addEventListener('click', () => {
                const userId = button.dataset.userId;
                const userName = button.dataset.userName;
                const userEmail = button.dataset.userEmail;
                const userRoles = JSON.parse(button.dataset.userRoles);

                UserRolesManager.fillEditModal(userId, userName, userEmail, userRoles);
            });
        });

        const addRoleButton = document.getElementById('add-role-button');
        if (addRoleButton) {
            addRoleButton.addEventListener('click', UserRolesManager.addRole);
        }
    },

    updateHiddenRolesField: () => {
        // Pobieramy wszystkie role z formularza
        const roles = document.querySelectorAll('.role-entry input');
        const rolesArray = Array.from(roles).map(role => role.value);
        // Przypisujemy wartości do ukrytego pola
        const rolesInput = document.querySelector('input[name="Roles"]');
        rolesInput.value = JSON.stringify(rolesArray);
    },

    init: () => {
        UserRolesManager.initEditButtons();
    }
};

document.addEventListener("DOMContentLoaded", () => {
    UserRolesManager.init();

    const form = document.querySelector('#EditUserRolesForm');
    if (form) {
        form.addEventListener('submit', (event) => {
            // Zaktualizowanie ukrytego pola 'Roles' przed wysłaniem formularza
            UserRolesManager.updateHiddenRolesField();
            console.log("Roles sent:", document.querySelector('input[name="Roles"]').value);
        });
    }

    document.getElementById('roles-container').addEventListener('click', function (event) {
        if (event.target.classList.contains('btn-remove-role')) {
            const entry = event.target.closest('.role-entry');
            entry.remove();

            // Zaktualizowanie indeksów wszystkich pól po usunięciu
            const roles = document.querySelectorAll('.role-entry input');
            roles.forEach((role, index) => {
                role.name = `Roles[${index}]`;
            });

            // Zaktualizowanie ukrytego pola
            UserRolesManager.updateHiddenRolesField();
        }
    });
});
