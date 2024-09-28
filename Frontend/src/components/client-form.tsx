"use client"

import { useState } from "react"
import { useMutation } from "@tanstack/react-query"
import { Wand2 } from "lucide-react"

import { useEnterSubmit } from "@/lib/hooks/use-enter-submit"
import { useForm } from "@/lib/hooks/use-form"

import { Textarea } from "./ui/textarea"

export default function ClientForm({
  declarationType,
}: {
  declarationType: string
}) {
  const [isInitialMessage, setIsInitialMessage] = useState(true)
  const { state, dispatch } = useForm()
  const [nextQuestion, setNextQuestion] = useState("")
  const [sentence, setSentence] = useState("")

  // Use useQuery for the initial message
  const { mutate: sendInitialMessage, isPending } = useMutation({
    mutationFn: async () => {
      const response = await fetch("http://192.168.137.19:49234/api/ask-tax-assistant", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
          userMessage: sentence,
          declarationType,
          isInitialMessage: true,
        }),
      })
      return response.json()
    },
  })

  const { formRef, onKeyDown } = useEnterSubmit()

  const handleSubmit = async () => {
    if (isInitialMessage) {
      sendInitialMessage(undefined, {
        onSuccess: (data) => {
          console.log(data);
          
          dispatch({ type: "SET_RESPONSE_DATA", payload: data.formState })
          setNextQuestion(data.nextQuestion)
          setIsInitialMessage(false)
        },
      })
    }
  }

  return (
    <div className="flex flex-col gap-5">
      <div className="text-2xl font-bold">
        <h3>Pomocnik AI - {declarationType}</h3>
      </div>
      <form
        ref={formRef}
        onSubmit={(e) => {
          e.preventDefault()
          handleSubmit()
        }}
        className="w-fullflex flex-col gap-5"
      >
        <label htmlFor="message">Message</label>
        <div className="flex flex-row">
          <Textarea
            value={sentence}
            onChange={(e) => setSentence(e.target.value)}
            onKeyDown={onKeyDown}
            placeholder="Your message"
            className="pr-14 text-base"
            rows={10}
          />
          <button type="submit" className="-ml-14">
            <Wand2 />
          </button>
        </div>
      </form>
      {nextQuestion && (
        <div className="mt-4">
          <h4 className="font-bold">Next Question:</h4>
          <p>{nextQuestion}</p>
        </div>
      )}
    </div>
  )
}
