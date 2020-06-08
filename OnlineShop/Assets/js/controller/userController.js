//
var lock_user = {
    init: function () {
        lock_user.registerEvents();
    },
    registerEvents: function () {
        $('.btn-blockuser').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = $(this).data('id')
            $.ajax({
                url: "/Admin/User/ChangeStatus",
                data: { username: id },
                đataType: "json",
                type : "POST", 
                success: function (response) {
                    
                    //alert(response)
                    if (response.status) {
                        btn.text("khóa người dùng")
                        $('.user-status').val("Đang hoạt động")
                    }
                    else {
                        btn.text("Bỏ khóa người dùng")
                        $('.user-status').val("Đã bị khóa")
                    }

                }
            })
        });

    }
}
lock_user.init()

var delete_user = {
    init: function () {
        delete_user.registerEvents();
    },
    registerEvents: function () {
        $('.delete-user').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = $(this).data('id')
            $.ajax({
                url: "/Admin/User/Delete",
                data: { username: id },
                đataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.delete) {
                        alert("Đã xóa người dùng "+ id )
                    }
                    else {
                        alert("Bạn không có quyền xóa tài khoản")
                    }
                    
                }
            })
        });

    }
}
delete_user.init()


function checkEmail() { 
    var email = document.getElementById('email'); 
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/; 
    if (!filter.test(email.value)) { 
        alert('Hay nhap dia chi email hop le.\nExample@gmail.com');
        email.focus; 
        return false; 
    }
    else{ 
        alert('OK roi day, Email nay hop le.'); 
    } 
} 

(function () {
    'use strict';
    window.addEventListener('load', function () {
        var forms = document.getElementsByClassName('needs-validation');
        var validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();