function checkPassword(str)
{
    var re = /^(?=.*\d)(?=.*[A-Z])(?=.*\W.*\W).{6,16}/;  
    return re.test(str);
}

$('#password').on('keyup blur', function(){

var passValue= $(this).val();
console.log( checkPassword(passValue) );

if(checkPassword(passValue)){
    $('#password').css('border','')
    $('#validpassword').addClass('d-block');
    $("#invalidpassword").addClass('d-none').removeClass('d-block');
}
else{
    $('#password').css('border','red 4px solid');
    $('#validpassword').removeClass('d-block').addClass('d-none');
    $("#invalidpassword").addClass('d-block').removeClass('d-none');
}

});


$('#confirmedPassword').on('keyup blur', function(){

    var confirmedpass = $('#confirmedPassword').val();
    
    console.log(confirmedpass);
    
    if(confirmedpass==$('#password').val()){
        $(this).css('border','');
        $('#matchedpassword').removeClass('d-none')
        $('#matchedpassword').addClass('d-block');
        
    }
    else{
        $(this).css('border','red 4px solid');
        $('#matchedpassword').removeClass('d-block')
        $('#matchedpassword').addClass('d-none');
    }
    
    });


    $('#ReferrerOther').on('click', function(){

        if ( $('#ReferrerOther').is(":checked")==true) {
            $('#otherReferrer').removeClass('d-none')
            $('#otherReferrer').addClass('d-block');
        } else {
            $('#otherReferrer').addClass('d-none');
            $('#otherReferrer').removeClass('d-block')
        }

    });

    $('.Referrer').on('click',function(){
        $('#checkboxes').css('border','');
        $('#uncheckedReferrer').addClass('d-none');
        $('#uncheckedReferrer').removeClass('d-block');
    })

    $('form').on('submit', function(){
        $('#successSubmit').addClass('d-none');

        if($('.Referrer').is(":checked")){
            $('#uncheckedReferrer').addClass('d-none');
            $('#uncheckedReferrer').removeClass('d-block');
            $('#checkboxes').css('border','');
            $('#successSubmit').removeClass('d-none');

            $("#successSubmit").text("✔ Thanks for signing up, "+$('#name').val()+"!");

           // return false;
        }
        else{
            $('#uncheckedReferrer').addClass('d-block');
            $('#uncheckedReferrer').removeClass('d-none');
            $('#checkboxes').css('border','red solid 4px')
             
         //   return false;
            
        }

    });

    $('form').on('reset', function () {
        $('#invalidpassword').removeClass('d-block').addClass('d-none');
        $('#matchedpassword').removeClass('d-block').addClass('d-none');
        $('#validpassword').removeClass('d-block').addClass('d-none');
        $('#successSubmit').removeClass('d-block').addClass('d-none');
        $('#checkboxes').css('border', '');
    });


    $('#addCoupon').on('click', function () {
        var CouponString = $('#CouponString').val();
        console.log('CouponString '+CouponString);
        $.ajax({ //Do an ajax post to the ASP controller
            type: 'POST',
            url: './Home/AddCoupon',
            data: { CouponString: CouponString },
            success: function (response) {
                if (response.success) {
                    alert(response.responseText);
                } else {
                    // DoSomethingElse()
                    alert(response.responseText);
                }
            },
            error: function (response) {
                alert("error!");  // 
            }
        });
    });

   
    





