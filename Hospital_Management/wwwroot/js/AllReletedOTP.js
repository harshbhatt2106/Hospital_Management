
export async function VerificationGmail(gmail) {
    try {
        const res = await fetch("/Admin/VerifyGmail",
            {
                method: "POST",
                headers:
                {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(gmail)
            })

        const result = await res.json();
        return result.success;
    }
    catch
    {
        throw new Error("NetWork Releted Issue")
    }
}

export async function SendOTP(gmail) {
    try {

        const res = await fetch("/Admin/SendOTP",
            {
                method: "POST",
                headers:
                {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(gmail)
            })
        const result = await res.json();
        return result.success;
    }
    catch
    {
        throw new Error("NetWork Releted Issue")
    }
}

export async function VeryfiOTP(_otp)
{
    
    // veryfy otp ajax call
    const res = await fetch("/Admin/VerifyOTP",
        {
            method: "Post",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(_otp)
        })

    const result = await res.json();
    return result;
}
