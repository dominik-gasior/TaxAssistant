'use server'
import { TFormData } from "@/app/types/steps"

export const restoreChat = async (storedNanoId: string) => {
    const host = process.env.NEXT_PUBLIC_BASE_URL
    console.log(host);

    const response = await fetch(
        host + `/restore-chat/${storedNanoId}`
    )
    if (!response.ok) throw new Error("Failed to restore chat")
    return response.json()
}

export const updateForm = async (storedNanoId: string, formData: TFormData) => {

    if (!storedNanoId) {
        throw new Error("Nano ID is required")
    }
    const host = process.env.NEXT_PUBLIC_BASE_URL
    console.log(host);


    const response = await fetch(
        host + `/update-form/${storedNanoId}`,
        {
            method: "PUT",
            body: JSON.stringify(formData),
        }
    )
    return response.json()
}
export const taxAssistant = async (nanoId: string, sentence: string, declarationType: string, isInitialMessage: boolean) => {
    const host = process.env.NEXT_PUBLIC_BASE_URL
    // if host is undefined log.error
    console.log(host);

    const response = await fetch(
        host + `/ask-tax-assistant/${nanoId}`,
        {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({
                userMessage: sentence,
                declarationType,
                isInitialMessage: isInitialMessage,
            }),
        }
    )
    return await response.json()
}


