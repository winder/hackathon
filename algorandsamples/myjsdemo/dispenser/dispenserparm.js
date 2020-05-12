


(function loadparm()
{
    const queryString = window.location.search;
    const urlParams = new URLSearchParams(queryString);
    $('#target').html(urlParams.get('account'));  
})();