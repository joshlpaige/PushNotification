self.addEventListener('push', function (e) {
    var body;
    console.log("Hello", e.data.text())
    if (e.data) {
        body = e.data.text();
    } else {
        body = "Standard Message";
    }

    var options = {
        body: body,
        icon: "images/icons/icon-512x512.png",
        vibrate: [100, 50, 100],
        data: {
            dateOfArrival: Date.now()
        },
        actions: [
            {
                action: "explore", title: "Go interact with this!",
                icon: "images/icons/icon-192x192.png"
            },
            {
                action: "close", title: "Ignore",
                icon: "images/icons/icon-192x192.png"
            },
        ]
    };
    e.waitUntil(
        self.registration.showNotification("Push Notification", options)
    );
});

self.addEventListener('notificationclick', function (e) {
    var notification = e.notification;
    var action = e.action;

    if (action === 'close') {
        notification.close();
    } else {
        // Some actions
        clients.openWindow('http://www.example.com');
        notification.close();
    }
});