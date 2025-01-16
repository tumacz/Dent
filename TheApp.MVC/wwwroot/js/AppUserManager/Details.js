document.addEventListener("DOMContentLoaded", function () {
    const userCards = document.querySelectorAll('.user-card');
    userCards.forEach(function (card) {
        const roles = JSON.parse(card.getAttribute('data-roles'));
        const availableRoles = JSON.parse(card.getAttribute('data-available-roles'));

        if (roles && roles.length > 0) {
            const rolesContainer = card.querySelector('.roles-list');
            rolesContainer.innerHTML = '';
            roles.forEach(function (role) {
                const listItem = document.createElement('li');
                listItem.className = 'list-group-item';
                listItem.textContent = role;
                rolesContainer.appendChild(listItem);
            });
        }
    });
});