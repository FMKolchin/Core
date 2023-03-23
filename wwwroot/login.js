const loginUri = '/Users/Login';

const varify = () => {
    let userName = document.getElementById('uname').value;
    let pswd = document.getElementById('pwd').value;
user = {id:"",userName:userName,password:pswd,classification:""}
fetch(loginUri, {
    method: 'POST',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
    },
    body: JSON.stringify(user)
})
.then(response =>{
    userName.value = '';
    pswd.value = '';
    if(response.ok)
   return response.text();
    else{
        throw new Error('Invalid');
    }
 })
.then((res) => {
   sessionStorage.setItem('auth',"Bearer ".concat(res.split('\"')[1]));

})
 .catch(error => window.location.href = "/error.html");
}