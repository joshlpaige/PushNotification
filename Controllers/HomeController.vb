Imports WebPush

Public Class HomeController
    Inherits System.Web.Mvc.Controller

    Function Index(client As String, endpoint As String, p256dh As String, auth As String) As ActionResult

        If client IsNot Nothing Then

            ' Organize Subscription object using keys
            Dim subscription As New PushSubscription(endpoint, p256dh, auth)
            'Save this to Database like this {client: subscription} so you can use this later

        End If
        Return View("Index")

    End Function

    Function Contact(message As String) As ActionResult

        ViewData("msg") = message ' testing purpose only

        ' Set VAPID variables (Read from config in Production)
        ' You can generate VAPID here: https://tools.reactpwa.com/vapid?email=joshuapaige1982%40gmail.com

        Dim subject As String = "mailto:joshuapaige1982@gmail.com"
        Dim publicKey As String = "BK3kj-8CTD3f2JS12_JHvq08Dq-7THHjZG1Ri2W99USD9CuoRHdpUDbk0FjwTO_lFU4BHFUKNH_nPlLtUoEtZb0"
        Dim privateKey As String = "c0NmD4kEMRkXpv_SVfrAyww66vu1gsNYCsX7Y-HUzkA"
        Dim vapidDetails As New VapidDetails(subject, publicKey, privateKey)

        ' Subscription Object. Read from DB in production 
        Dim pushEndpoint As String = "https://fcm.googleapis.com/fcm/send/feTlpkjXjpM:APA91bHHfuNUOf5m9bXzdE_wVZPLWNlzRDN57bO5oyVAV6AWlnH1AJrvNA5lieZ3c1Vx3VLx3vZBIOw6DU_8kEK6j_7QZtIf230-ReRG0m6P0QLkgI0SzWu-d13qetlnprIn0oNXXM1z"
        Dim p256dh As String = "BFtfnI9pbHcUmASopQsKCnBoIMoJjHWNV5GhK9hYRBp/udWrlbJzoFFdoxZXBe55lZwGRSIHWS/wYWRo/aifadQ="
        Dim auth As String = "L943kdcCz0l/JcU7IRPzwA=="
        Dim subscription As New PushSubscription(pushEndpoint, p256dh, auth)


        ' Webpush client instance
        Dim webPushClient As New WebPushClient()
        webPushClient.SendNotification(subscription, message, vapidDetails)
        Return View("Contact")
    End Function
End Class
