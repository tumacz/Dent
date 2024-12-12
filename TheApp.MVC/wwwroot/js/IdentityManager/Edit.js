document.getElementById('btn-add-role').addEventListener('click', function () {
    const container = document.getElementById('roles-container');
    const currentRoles = container.querySelectorAll('.role-entry input');
    const roleIndex = currentRoles.length; // Ustal nowy indeks

    const roleEntry = document.createElement('div');
    roleEntry.className = 'input-group mb-2 role-entry';

    roleEntry.innerHTML = `
        <input name="Roles[${roleIndex}]" class="form-control" />
        <button type="button" class="btn btn-danger btn-remove-role">Remove</button>
    `;

    container.appendChild(roleEntry);
});

document.getElementById('roles-container').addEventListener('click', function (event) {
    if (event.target.classList.contains('btn-remove-role')) {
        const entry = event.target.closest('.role-entry');
        entry.remove();

        // Zaktualizuj indeksy wszystkich pól po usunięciu
        const roles = document.querySelectorAll('.role-entry input');
        roles.forEach((role, index) => {
            role.name = `Roles[${index}]`;
        });
    }
});

