function showManualModal(title, message, isSuccess = true, redirectUrl = null) {
    var modal = document.getElementById('manualModal');
    var modalTitle = document.getElementById('manualModalTitle');
    var modalMessage = document.getElementById('manualModalMessage');
    var modalIcon = document.getElementById('manualModalIcon');
    var modalBtn = document.getElementById('manualModalBtn');

    var friendlyMessage = message;
    if (typeof message === 'string' && message.includes('UNIQUE KEY constraint')) {
        var match = message.match(/\(([^)]+)\)/);
        var duplicateValue = match ? match[1] : 'This value';
        friendlyMessage = `${duplicateValue} is already registered. Please use a different one.`;
    }

    modalTitle.textContent = title || (isSuccess ? 'Success' : 'Error');
    modalMessage.textContent = friendlyMessage;

    if (isSuccess) {
        modalIcon.className = 'custom-modal-icon success';
        modalIcon.innerHTML = '<i class="fas fa-check-circle"></i>';
    } else {
        modalIcon.className = 'custom-modal-icon error';
        modalIcon.innerHTML = '<i class="fas fa-exclamation-circle"></i>';
    }

    modal.classList.add('active');

    modalBtn.onclick = function () {
        modal.classList.remove('active');
        if (redirectUrl) {
            window.location.href = redirectUrl;
        }
    };
}