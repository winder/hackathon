

(function loadparm()
{
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    const parmTarget = urlParams.get('account');
    var target1 = document.getElementById('target');
    target1.value = "";
    target1.value = parmTarget;
    var $target = $('target');
    $target.html(target1.value);
})();