/**
 * Password Toggle Functionality
 * Reusable script for show/hide password functionality
 */

(function() {
    'use strict';

    /**
     * Initialize password toggle functionality
     */
    function initPasswordToggle() {
        const passwordTogglers = document.querySelectorAll('.form-password-toggle .input-group-text');
        
        passwordTogglers.forEach(function(toggler) {
            toggler.addEventListener('click', function() {
                const passwordInput = this.parentElement.querySelector('input');
                const icon = this.querySelector('i');
                
                if (passwordInput && icon) {
                    togglePasswordVisibility(passwordInput, icon);
                }
            });
        });
    }

    /**
     * Toggle password visibility
     * @param {HTMLInputElement} input - The password input element
     * @param {HTMLElement} icon - The icon element
     */
    function togglePasswordVisibility(input, icon) {
        if (input.type === 'password') {
            input.type = 'text';
            icon.classList.remove('bx-hide');
            icon.classList.add('bx-show');
        } else {
            input.type = 'password';
            icon.classList.remove('bx-show');
            icon.classList.add('bx-hide');
        }
    }

    // Initialize when DOM is ready
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', initPasswordToggle);
    } else {
        initPasswordToggle();
    }

    // Expose to global scope if needed
    window.PasswordToggle = {
        init: initPasswordToggle,
        toggle: togglePasswordVisibility
    };

})();