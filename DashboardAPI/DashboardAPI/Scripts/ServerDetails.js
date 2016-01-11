var apps = '';
var isAjaxCompleted = true;
$(function () {
    var selectedAppId = '';

    var data = '';
    getAppData('');
    //autocomplete feature

    //search button
    $('#btnSearch').click(function () {
        getAppData(selectedAppId);
        return false;
    });

    var map = {};
    var substringMatcher = function (strs) {
        return function findMatches(q, cb) {
            var matches, substringRegex;

            // an array that will be populated with substring matches
            matches = [];

            // regex used to determine if a string contains the substring `q`
            substrRegex = new RegExp(q, 'i');

            // iterate through the pool of strings and for any string that
            // contains the substring `q`, add it to the `matches` array
            $.each(strs, function (i, str) {
                if (substrRegex.test(str.AppName)) {
                    map[str.AppName] = str;
                    matches.push(str.AppName);
                }
            });
            cb(matches);
        };
    };

    apps = [{ "AppName": "appsvc.mcafee.com", "AppId": "129" },
                { "AppName": "ae.mcafee.com", "AppId": "130" },
                { "AppName": "apiservices.int.mcafee.com", "AppId": "131" },
                { "AppName": "app.int.mcafee.com", "AppId": "132" },
                { "AppName": "app.mcafee.com", "AppId": "133" },
                { "AppName": "appsvc.ext.mcafee.com", "AppId": "134" },
                { "AppName": "appsvc.int.mcafee.com", "AppId": "135" },
                { "AppName": "au.mcafee.com", "AppId": "136" },
                { "AppName": "br.mcafee.com", "AppId": "137" },
                { "AppName": "ca.mcafee.com", "AppId": "138" },
                { "AppName": "cart.mcafee.com", "AppId": "139" },
                { "AppName": "cn.mcafee.com", "AppId": "140" },
                { "AppName": "cs-smbstore.mcafee.com", "AppId": "141" },
                { "AppName": "cs.mcafee.com", "AppId": "142" },
                { "AppName": "cz.mcafee.com", "AppId": "143" },
                { "AppName": "de.mcafee.com", "AppId": "144" },
                { "AppName": "dk.mcafee.com", "AppId": "145" },
                { "AppName": "es.mcafee.com", "AppId": "146" },
                { "AppName": "fi.mcafee.com", "AppId": "147" },
                { "AppName": "fr.mcafee.com", "AppId": "148" },
                { "AppName": "gr.mcafee.com", "AppId": "149" },
                { "AppName": "home.internal.mcafee.com", "AppId": "150" },
                { "AppName": "home.mcafee.com", "AppId": "151" },
                { "AppName": "homeweb.mcafee.com", "AppId": "152" },
                { "AppName": "hr.mcafee.com", "AppId": "153" },
                { "AppName": "hu.mcafee.com", "AppId": "154" },
                { "AppName": "il.mcafee.com", "AppId": "155" },
                { "AppName": "it.mcafee.com", "AppId": "156" },
                { "AppName": "jj.au.mcafee.com", "AppId": "157" },
                { "AppName": "jp.mcafee.com", "AppId": "158" },
                { "AppName": "kr.mcafee.com", "AppId": "159" },
                { "AppName": "malware.mcafeesecure.com", "AppId": "160" },
                { "AppName": "mcafeecloudsecure.com", "AppId": "161" },
                { "AppName": "mcafeesecure.com", "AppId": "162" },
                { "AppName": "mcasyncsvc.int.mcafee.com", "AppId": "163" },
                { "AppName": "mcloud.int.mcafee.com", "AppId": "164" },
                { "AppName": "mcloud.mcafee.com", "AppId": "165" },
                { "AppName": "mcsvc.mcafee.com", "AppId": "166" },
                { "AppName": "mx.mcafee.com", "AppId": "167" },
                { "AppName": "nl.mcafee.com", "AppId": "168" },
                { "AppName": "no.mcafee.com", "AppId": "169" },
                { "AppName": "partnersvc.mcafee.com", "AppId": "170" },
                { "AppName": "partnersvcacl.netops.mcafee.com", "AppId": "171" },
                { "AppName": "payment.mcafee.com", "AppId": "172" },
                { "AppName": "pl.mcafee.com", "AppId": "173" },
                { "AppName": "posactivation.mcafee.com", "AppId": "174" },
                { "AppName": "preprod-eol-int.mcafee.com", "AppId": "175" },
                { "AppName": "preprod-eol-us.mcafee.com", "AppId": "176" },
                { "AppName": "preprod-int.mcafee.com", "AppId": "177" },
                { "AppName": "preprod-us.mcafee.com", "AppId": "178" },
                { "AppName": "pt.mcafee.com", "AppId": "179" },
                { "AppName": "puppetmaster.mcafeesecure.com", "AppId": "180" },
                { "AppName": "renewal-prod.mcafee.com", "AppId": "181" },
                { "AppName": "rs.mcafee.com", "AppId": "182" },
                { "AppName": "rsakm.mcafee.com", "AppId": "183" },
                { "AppName": "ru.mcafee.com", "AppId": "184" },
                { "AppName": "salesdb.mcafeesecure.com", "AppId": "185" },
                { "AppName": "scanalert.mcafee.com", "AppId": "186" },
                { "AppName": "scanrpc.mcafeesecure.com", "AppId": "187" },
                { "AppName": "se.mcafee.com", "AppId": "188" },
                { "AppName": "secure.parentalctrl.com", "AppId": "189" },
                { "AppName": "secureservice.mcafee.com", "AppId": "190" },
                { "AppName": "shop.mcafee.com", "AppId": "191" },
                { "AppName": "signon.mcafee.com", "AppId": "192" },
                { "AppName": "sk.mcafee.com", "AppId": "193" },
                { "AppName": "smbservices.mcafee.com", "AppId": "194" },
                { "AppName": "smbws.mcafee.com", "AppId": "195" },
                { "AppName": "svc.int.mcafee.com", "AppId": "196" },
                { "AppName": "tr.mcafee.com", "AppId": "197" },
                { "AppName": "trevance.int.mcafee.com", "AppId": "198" },
                { "AppName": "tw.mcafee.com", "AppId": "199" },
                { "AppName": "uk.mcafee.com", "AppId": "200" },
                { "AppName": "us.mcafee.com", "AppId": "201" },
                { "AppName": "wildcard.hackersafe.com", "AppId": "202" },
                { "AppName": "wildcard.mcafeecloudsecure.com", "AppId": "203" },
                { "AppName": "wildcard.mcafeesecure.com", "AppId": "204" },
                { "AppName": "wildcard.scanalert.com", "AppId": "205" },
                { "AppName": "ws.mcafee.com", "AppId": "206" },
                { "AppName": "xss_us.mcafee.com", "AppId": "207" },
                { "AppName": "xx.mcafee.com", "AppId": "208" },
                { "AppName": "admin.hackerwatch.org", "AppId": "209" },
                { "AppName": "blui.int.mcafee.com", "AppId": "210" },
                { "AppName": "ceg.int.mcafee.com", "AppId": "211" },
                { "AppName": "cs-sdlhash.mcafee.com", "AppId": "212" },
                { "AppName": "denied.mcafee.com", "AppId": "213" },
                { "AppName": "ebizadmin.mcafee.com", "AppId": "214" },
                { "AppName": "edgescape.mcafee.com", "AppId": "215" },
                { "AppName": "instrumq.beta.mcafee.com", "AppId": "216" },
                { "AppName": "int-log.mcafeesecure.com", "AppId": "217" },
                { "AppName": "legacy.edgescape.mcafee.com", "AppId": "218" },
                { "AppName": "mqinstru.int.mcafee.com", "AppId": "219" },
                { "AppName": "mqpool1.int.mcafee.com", "AppId": "220" },
                { "AppName": "mqpool2.int.mcafee.com", "AppId": "221" },
                { "AppName": "mqpool3.int.mcafee.com", "AppId": "222" },
                { "AppName": "mtsvc.int.mcafee.com", "AppId": "223" },
                { "AppName": "pricewizard.mcafee.com", "AppId": "224" },
                { "AppName": "radar.corp.mcafee.com", "AppId": "225" },
                { "AppName": "sdlhash.mcafee.com", "AppId": "226" },
                { "AppName": "sjv-dns.mcafeesecure.com", "AppId": "227" },
                { "AppName": "sjv-mx.mcafeesecure.com", "AppId": "228" },
                { "AppName": "sjv-proxy.mcafeesecure.com", "AppId": "229" },
                { "AppName": "sjvdrappsapint.int.mcafee.com", "AppId": "230" },
                { "AppName": "smc.mcafee.com", "AppId": "231" },
                { "AppName": "sminstru.mcafee.com", "AppId": "232" },
                { "AppName": "support.int.mcafee.com", "AppId": "233" },
                { "AppName": "taxware.int.mcafee.com", "AppId": "234" },
                { "AppName": "taxware.mcafee.com", "AppId": "235" },
                { "AppName": "telemetry.int.mcafee.com", "AppId": "236" },
                { "AppName": "util.mcafee.com", "AppId": "237" },
                { "AppName": "yapsvc.mcafee.com", "AppId": "238" },
                { "AppName": "yapsvc.int.mcafee.com", "AppId": "239" },
                { "AppName": "identitysvc.int.mcafee.com", "AppId": "240" },
                { "AppName": "yap.mcafee.com", "AppId": "241" },
                { "AppName": "yapoauth.int.mcafee.com", "AppId": "242" },
                { "AppName": "yapoauth.mcafee.com", "AppId": "243" },
                { "AppName": "yapidx.int.mcafee.com", "AppId": "244" },
                { "AppName": "truekey.intelsecurity.com", "AppId": "245" },
                { "AppName": "truekeyapi.intelsecurity.com", "AppId": "246" },
                { "AppName": "truekeyemail.int.intelsecurity.com", "AppId": "247" },
                { "AppName": "pabe.int.mcafee.com", "AppId": "248" },
                { "AppName": "truekeycart.intelsecurity.com", "AppId": "249" },
                { "AppName": "platform.mcafee.com", "AppId": "250" },
                { "AppName": "platformapi.int.mcafee.com", "AppId": "251" },
                { "AppName": "platformsvc.int.mcafee.com", "AppId": "252" },
                { "AppName": "platformwf.int.mcafee.com", "AppId": "253" },
                { "AppName": "rsadpm.int.mcafee.com", "AppId": "254" },
                { "AppName": "vcapi.mcafee.com", "AppId": "255" }
    ];

    $('input.typeahead').typeahead({
        hint: true,
        highlight: true,
        minLength: 1
    },
    {
        name: 'states',
        source: substringMatcher(apps)
    });
    $('input.typeahead').on('typeahead:selected', function (evt, item) {
        selectedAppId = map[item].AppId;
        $('#btnSearch').removeClass('disabled');
    });

    $(document).on("mouseenter", '.panel-heading a', function () {
        $('#dvHostFile').html('Hover on server name to see host file.');
        var left = $(this).offset().left + $(this).width();
        if (left > ($(window).width() - $('#dvHostFile').width())) {
            left = $(this).offset().left - $('#dvHostFile').width();
        }
        var divToShow = $('#dvHostFile').show();
        divToShow.removeClass('hide');
        divToShow.css({
            display: "block",
            left: left + "px",
            top: $(this).offset().top + "px"
        });
    }).on("mouseleave", '.panel-heading a', function () {
        $('#dvHostFile').hide();
    });
});

function collapseGroup(obj) {
    var isCollapsing = false;
    var collapseDiv = $(obj).closest('.row');
    var toggleClass = $(obj).attr('collapseClass');

    $('.' + toggleClass).each(function (index) {
        if ($(this).hasClass('in')) {
            $(this).removeClass('in');
            isCollapsing = true;
            var rowDiv = $(collapseDiv).closest('.row');
            $('.text-center', rowDiv).each(function () {
                $(this).removeAttr('style');
            })
        }
        else {
            $(this).addClass('in');
        }
    });
    if (!isCollapsing) {
        
        var maxHeight = -1;
        $('.sameHeight', collapseDiv).each(function () {
            maxHeight = maxHeight > $(this).height() ? maxHeight : $(this).height();
        });
        $('.sameHeight', collapseDiv).each(function () {
            $(this).height(maxHeight);
        });

        var dMaxHeight = -1;
        $('.datacenter', collapseDiv).each(function () {
            dMaxHeight = dMaxHeight > $(this).height() ? dMaxHeight : $(this).height();
        });
        $('.datacenter', collapseDiv).each(function () {
            $(this).height(dMaxHeight);
        });

        var rMaxHeight = -1;
        $('.text-center', collapseDiv).each(function () {
            rMaxHeight = rMaxHeight > $(this).height() ? rMaxHeight : $(this).height();
        });
        $('.text-center', collapseDiv).each(function () {
            $(this).height(rMaxHeight);
        });
    }
}

function setHostFileContent(content) {
    var entries = content.split('\r\n');
    var hostFileContent = '';
    hostFileContent += '<div style="float: left;">';
    $.each(entries, function (index, value) {
        if (value != '') {
            hostFileContent += value + '<br/>';
        }
    });
    hostFileContent += '</div>';
    hostFileContent += '<div class="close"><button type="button" onclick="closeHostDiv()">×</button></div>'
    $('#dvHostFile').append(hostFileContent);
}

function getAppData(appid) {
    //$('.jumbotron *').not(".page-header *").empty();
    $('#appContent').empty();
    $('.progress').show();
    $.getJSON('api/LB/' + appid)
          .done(function (aData) {

              //---remove
            
              //--remove
              // On success, 'adata' contains a list of applications.
              $('.progress').hide();
              data = {
                  "Servers": aData
              };


              //populating server details
              var output = '';
              var counter = 0;
              $.each(data.Servers, function (index, sValue) {
                  if (index % 2 == 0) {
                      output += '<div class="row">';
                  }
                  output += '<div class="text-center col-md-6 panel panel-primary">';
                  var appName = '';
                  $.each(apps, function (index, appData) {
                      if (appData.AppId == sValue.ApplicationName) {
                          appName = appData.AppName;
                      }
                  });
                  output += '<div class="panel-heading"><h3><a  onclick="collapseGroup(this)" collapseClass="collapse' + counter + '" href="#collapse' + counter + '">' + appName + '</a></h3></div>';
                  output += '<div  class="panel-body panel-collapse collapse collapse' + counter + '" style="height: 0px;">';


                  $.each(sValue.DC, function (index, dValue) {
                      output += '<div class="datacenter col-md-6 panel panel-primary">';

                      output += '<div class="datacenterHeading"><h3>' + dValue.DataCenterName + '</h3></div>';
                      output += '<div class="panel-body ">';

                      if (dValue.hasOwnProperty('ProdNodes') && Object.keys(dValue.ProdNodes).length > 0) {
                          if (dValue.hasOwnProperty('StgNodes') && Object.keys(dValue.StgNodes).length > 0) {
                              output += '<div class="sameHeight col-md-6 panel panel-primary">';
                          }
                          else {
                              output += '<div class="sameHeight col-md-6 panel panel-primary single">';
                          }
                          output += '<div class=" panel-heading"><h4 class="panel-title">PROD</h4></div>';
                          output += '<div class="panel-body">';
                          output += '<ul class="list-inline">';

                          $.each(dValue.ProdNodes, function (key, value) {
                              var imageUrl = "Images/active.jpg";
                              if (value.toUpperCase() == "SESSION_STATUS_FORCED_DISABLED") {
                                  imageUrl = "Images/inactive.jpg";
                              }
                              var dotindex = key.indexOf(".");
                              var serName = key;
                              if (dotindex != -1) {
                                  serName = key.substr(0, dotindex);
                              }
                              output += "<li>" + "<img class='img-thumbnail' src='" + imageUrl + "'/>" + serName.toUpperCase() + "</li>";
                          });
                          output += "</ul>";
                          output += "</div>";
                          output += "</div>";
                      }

                      if (dValue.hasOwnProperty('StgNodes') && Object.keys(dValue.StgNodes).length > 0) {
                          if (dValue.hasOwnProperty('ProdNodes') && Object.keys(dValue.ProdNodes).length > 0) {
                              output += '<div class="sameHeight col-md-6 panel panel-primary">';
                          }
                          else {
                              output += '<div class="sameHeight col-md-6 panel panel-primary single">';
                          }
                          output += '<div class="panel-heading"><h4 class="panel-title">STG</h4></div>';
                          output += '<div class="panel-body">';
                          output += '<ul class="list-inline">';
                          $.each(dValue.StgNodes, function (key, value) {
                              var imageUrl = "Images/active.jpg";
                              if (value.toUpperCase() == "SESSION_STATUS_FORCED_DISABLED") {
                                  imageUrl = "Images/inactive.jpg";
                              }
                              var dotindex = key.indexOf(".");
                              var serName = key;
                              if (dotindex != -1) {
                                  serName = key.substr(0, dotindex);
                              }
                              output += "<li>" + "<img class='img-thumbnail' src='" + imageUrl + "'/>" + serName.toUpperCase() + "</li>";
                          });
                          output += "</ul>";
                          output += "</div>";
                          output += "</div>";
                      }

                      output += "</div>";
                      output += "</div>";
                  });
                  output += "</div>";
                  output += "</div>";

                  if (index % 2 != 0) {
                      output += '</div>';
                      counter += 1;
                  }
              });
              $('#appContent').append(output);

              //show host file on hover
              $('.jumbotron li').hover(function () {
                  $('#dvHostFile').html('');
                  var content = "";
                  var serverName = $(this).text();

                  // Send an AJAX request
                  content = $("#dvHostFile").data(serverName);
                  if (typeof content === "undefined") {
                      if (isAjaxCompleted) {
                          isAjaxCompleted = false;
                          content = '';
                          $.getJSON('GetHosts/' + serverName)
                              .done(function (data) {
                                  isAjaxCompleted = true;
                                  // On success, 'data' contains a list of hosts.
                                  $.each(data, function (key, item) {
                                      // Add a list item for the product.
                                      content += item;
                                      $("#dvHostFile").data(serverName, content);
                                      setHostFileContent(content);
                                  });
                              });
                      }
                  }
                  else {
                      setHostFileContent(content);
                  }
                  var left = $(this).offset().left + $(this).width();
                  if (left > ($(window).width() - $('#dvHostFile').width())) {
                      left = $(this).offset().left - $('#dvHostFile').width();
                  }
                  var divToShow = $('#dvHostFile').show();
                  divToShow.removeClass('hide');
                  divToShow.css({
                      display: "block",
                      left: left + "px",
                      top: $(this).offset().top + "px"
                  });
              });

              
          });
}

function closeHostDiv() {
    $('#dvHostFile').hide();
}


