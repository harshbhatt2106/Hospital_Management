const _newpassword = document.getElementById('Password');
const _ConfirmPassword = document.getElementById('ConfirmPassword');
const oldpassword = document.getElementById('oldPassword');

const data = document.getElementById('error');
const btn = document.getElementById('btn');

_ConfirmPassword.addEventListener('input', () => {
    const np = _newpassword.value;
    const cf = _ConfirmPassword.value;

    if (np !== cf) {
        data.textContent = "❌ Password does not match";
        btn.disabled = true;
        btn.classList.remove('bg-indigo-600', 'hover:bg-indigo-700');
        btn.classList.add('bg-gray-400', 'cursor-not-allowed');
    }
    else {
        data.textContent = "";
        btn.disabled = false;
        btn.classList.add('bg-indigo-600', 'hover:bg-indigo-700');
        btn.classList.remove('bg-gray-400', 'cursor-not-allowed');
    }
});