window.onload = function () {
    var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjYxYzI1MDA4LTJhMjgtNGY1Ni04ZGRiLTQzMjYyNTQxZjA1NCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJhZG1pbiIsImp0aSI6ImVlNWZhMWVjLTAzNzItNDkxZC05YjRmLTNiOGZlNjMyMTQ1NSIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzM5NzE3OTk3fQ.DgaoI8T9ei_Ta-P-443rN158wEfsEawhpZiRi4pc7yU";  // مقدار توکن پیش‌فرض را اینجا وارد کنید
    if (token) {
        setTimeout(() => {
            var authButton = document.querySelector('.authorize');
            if (authButton) {
                authButton.click();
                setTimeout(() => {
                    var input = document.querySelector('input[type="text"]');
                    if (input) {
                        input.value = "Bearer " + token;
                        var okButton = document.querySelector('.btn.modal-btn.auth.btn-done');
                        if (okButton) {
                            okButton.click();
                        }
                    }
                }, 1000);
            }
        }, 1000);
    }
};