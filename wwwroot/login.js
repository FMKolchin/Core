const loginUri = '/Users/Login';
// const user = parseJwt(token.split(' ')[1]);
// const classification = user.Classification;

const varify = () => {
    let userName = document.getElementById('uname');
    let pswd = document.getElementById('pwd');
    user = { id: "", userName: userName.value, password: pswd.value, classification: "" }
    fetch(loginUri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(user)
    })
        .then(response => {
            userName.value = '';
            pswd.value = '';
            if (response.ok) {
                return response.json();
            } else
                throw new Error('Invalid');

        }).then((res => {
            sessionStorage.setItem('user', res.user);
            sessionStorage.setItem('auth', "Bearer " + res.token);
            if (res.classification === "agent")
                window.location.href = "/TaskManagement.html";
            else
                window.location.href = "/UserManagement.html";
        }))
        .catch(error => window.location.href = "/error.html");
}