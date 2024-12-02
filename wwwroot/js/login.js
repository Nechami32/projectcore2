const uri="http://localhost:5071/login"
console.log("in login!!!!!!!!!!!!!!");
const dom = {
    name: document.getElementById("name"),
    password: document.getElementById("password"),
    submitBtn: document.getElementById("submit")
}
dom.submitBtn.onclick = (event) => {
    event.preventDefault();
    console.log(dom);
    console.log("1");

    const item = { Username: dom.name.value, Password: (dom.password.value).toString() };

console.log(item);

fetch(uri, {
    method: 'POST',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
    },
    body: JSON.stringify(item)
})
    .then(response => {
        if (!response.ok) {
            // אם התגובה לא תקינה, הצג הודעה בהתאם
            if (response.status === 400) {
                alert("Bad Request: Check the format of the data sent.");
            } else if (response.status === 401) {
                alert("Unauthorized: The username or password is incorrect.");
            }
            throw new Error(`HTTP error! Status: ${response.status}`);
        }
        return response.json(); // המשך לעבד את ה-JSON רק אם הסטטוס תקין
    })
    .then(data => {
        console.log(data.token); // הדפסת הטוקן
console.log(data);
        // שמירת נתונים ב-LocalStorage
        localStorage.setItem("token", data.token);
        localStorage.setItem('userId', data.id);

        // מעבר לדף הבא
        location.href = "../index.html";
    })
    .catch(error => {
        console.error('Error:', error);
    });



























}