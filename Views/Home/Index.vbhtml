@Code
    ViewData("Title") = "Home Page"
End Code

<script>
        if ('serviceWorker' in navigator) {
            window.addEventListener("load", () => {
                // Register Service Worker
                navigator.serviceWorker.register("/ServiceWorker.js")
                    .then((reg) => {
                        if (Notification.permission === "granted") {
                            // Notification Enabled
                            $("#form").show();
                            getSubscription(reg);
                        } else if (Notification.permission === "blocked") {
                            $("#NoSupport").show();
                        } else {
                            // Ask permission
                            $("#GiveAccess").show();
                            $("#PromptForAccessBtn").click(() => requestNotificationAccess(reg));
                        }
                    });
            });
        } else {
            $("#NoSupport").show();
        }

        function requestNotificationAccess(reg) {
            Notification.requestPermission(function (status) {
                $("#GiveAccess").hide();
                if (status == "granted") {
                    $("#form").show();
                    getSubscription(reg);
                } else {
                    $("#NoSupport").show();
                }
            });
        }

        function getSubscription(reg) {
            reg.pushManager.getSubscription().then(function (sub) {
                if (sub === null) {
                    reg.pushManager.subscribe({
                        userVisibleOnly: true,
                        // applicationServerky: VAPID['publicKey']
                        applicationServerKey: "BK3kj-8CTD3f2JS12_JHvq08Dq-7THHjZG1Ri2W99USD9CuoRHdpUDbk0FjwTO_lFU4BHFUKNH_nPlLtUoEtZb0"
                    }).then(function (sub) {
                        // Add hidden values
                        fillSubscribeFields(sub);
                    }).catch(function (e) {
                        console.error("Unable to subscribe to push", e);
                    });
                } else {
                    fillSubscribeFields(sub);
                }
            });
        }

        function fillSubscribeFields(sub) {
            console.log(arrayBufferToBase64(sub.getKey("p256dh")))
            const keys = {
                endpoint: sub.endpoint, 
                p256dh: arrayBufferToBase64(sub.getKey("p256dh")),
                auth: arrayBufferToBase64(sub.getKey("auth"))
            }
            // Demo purpose only
            $("#sub_end").text(keys.endpoint);
            $("#p_key").text(keys.p256dh);
            $("#auth_key").text(keys.auth);

            // Add Hidden values
            $("#endpoint").val(sub.endpoint);
            $("#p256dh").val(arrayBufferToBase64(sub.getKey("p256dh")));
            $("#auth").val(arrayBufferToBase64(sub.getKey("auth")));
        }
        

        function arrayBufferToBase64(buffer) {
            var binary = '';
            var bytes = new Uint8Array(buffer);
            var len = bytes.byteLength;
            for (var i = 0; i < len; i++) {
                binary += String.fromCharCode(bytes[i]);
            }
            return window.btoa(binary);
        }
</script>

<main>

    <h1 style="margin-top:5rem" id="title">Subscribe to Push Notifications</h1>
    <div id="GiveAccess" style="display:none;">
        Give access to making notifications:
        <button id="PromptForAccessBtn">Prompt</button>
    </div>
    <div id="NoSupport" style="display:none;">
        Your browser does not support Push Notifications or you have blocked notifications
    </div>

    <div>
        Endpoint: <span id="sub_end"></span><br /><br />
        p256 Key: <span id="p_key"></span><br /><br />
        Auth Token: <span id="auth_key"></span><br />
    </div>

    <form asp-action="Index" id="form" style="display:none;">
        <label for="client">Your name: </label>
        <input id="client" name="client" />

        <input id="endpoint" name="endpoint" hidden />
        <input id="p256dh" name="p256dh" hidden />
        <input id="auth" name="auth" hidden />

        <button type="submit" class="btn btn-primary btn-md">Subscribe</button>
    </form>


</main>
