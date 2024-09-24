//// wwwroot/js/register.js

document.addEventListener('DOMContentLoaded', function () {
    const element = document.getElementById('myElement');
    if (element) {
        element.addEventListener('click', function () {
            console.log('Element clicked');
        });
    }
});


//document.addEventListener('DOMContentLoaded', function () {
//    document.getElementById('register-button').addEventListener('click', async function () {
//        const username = document.getElementById('register-username').value;
//        const email = document.getElementById('register-email').value;
//        const password = document.getElementById('register-password').value;
//        const confirmPassword = document.getElementById('register-confirm-password').value;
//        const privacyPolicyChecked = document.getElementById('register-privacy-policy').checked;

//        if (!privacyPolicyChecked) {
//            alert('You must agree to the privacy policy and terms.');
//            return;
//        }

//        if (password !== confirmPassword) {
//            alert('Passwords do not match.');
//            return;
//        }

//        try {
//            const data = {
//                Email: email,
//                Password: password,
//                EmergencyContactDetails: JSON.stringify({ phone: "123-456-7890", name: "John Doe" })
//            };

//            const response = await fetch('/api/Account/Register', {
//                method: 'POST',
//                headers: {
//                    'Content-Type': 'application/json',
//                },
//                body: JSON.stringify(data),
//            });

//            if (response.ok) {
//                const result = await response.text();
//                alert('User registered successfully. Check your email: ' + result);
//                window.location.href = '/login'; // Redirect to login page or elsewhere
//            } else {
//                console.log('API call failed with status code: ' + response.status);
//                alert('Registration failed. Please try again.');
//            }
//        } catch (error) {
//            console.log('An error occurred: ' + error.message);
//            alert('An error occurred. Please try again.');
//        }
//    });
//});
