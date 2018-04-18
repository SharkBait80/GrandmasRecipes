const apiUrl='https://uecp6o1809.execute-api.ap-southeast-2.amazonaws.com/Prod/API/Recipe';

const getTokenUrl='https://dxeup5xarj.execute-api.ap-southeast-2.amazonaws.com/Prod/API/Token';

const requiresAuth=true;

var securityToken='';

$(document).ready(function(){
    $("#p_Recipe").hide();
    $("#button_ClearToken").hide();
    $("#p_Loader").hide();
})


$(document).on("click","#button_GetRecipe",function(e){
    e.preventDefault();
   
    if (requiresAuth==true && securityToken=='')
         refreshSecurityToken().then(function(){
             getRecipe();
         });
    else
         getRecipe();
  
})

$(document).on("click","#button_ClearToken",function(e){
    e.preventDefault();

    securityToken='';
    $("#p_Recipe").hide();
    alert('Security token cleared.');
})

function getRecipe()
{
  
    $.ajax({
        method:'GET',
        datatype:'application/json',
        url: apiUrl,
        beforeSend : function(xhr) {
            $("#p_Loader").show();
            if (requiresAuth)
                xhr.setRequestHeader("Authorization",  securityToken);
        }
       
      }).done(function(data) {
        $("#p_Loader").hide();
        $("#p_Recipe").show();
        $("#label_Recipe").text(data.name);
        $("#text_Recipe").val(data.recipeText.join('\n'));
    })
    .fail(function( jqXHR, textStatus, errorThrown ){
        $("#p_Loader").hide();
        // Clear the security token in case it has become stale
        securityToken='';
        $("#button_ClearToken").hide();
        if (jqXHR.statusText)
             alert("An error occurred: " + jqXHR.statusText);
        else
            alert("An error occurred");
        reject();
    });
}

function refreshSecurityToken()
{
    return new Promise(function(resolve,reject){
    var enteredPassword=prompt("Please enter the password to access Grandma's recipes.");

    if (enteredPassword!=null && enteredPassword != '')
    {
        var dataToPost= {"": enteredPassword};
        $("#p_Loader").show();
    $.ajax({
        method:'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/x-www-form-urlencoded'
          },
          async: false,
        url: getTokenUrl,
        data:dataToPost
      }).done(function(data) {
        $("#p_Loader").hide();
        securityToken=data.token;
        $("#button_ClearToken").show();
        resolve();
    }).fail(function( jqXHR, textStatus, errorThrown ){
        $("#p_Loader").hide();
        if (jqXHR.responseText)
             alert("An error occurred: " + jqXHR.responseText);
        else
            alert("An error occurred");
        reject();
    });
    }
    else
     {   alert("Please enter a password to access Grandma's recipes.");

       reject();
    }
});
}