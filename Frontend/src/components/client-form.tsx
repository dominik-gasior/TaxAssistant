"use client"

import { useEffect, useState } from "react"
import { useMutation, useQuery } from "@tanstack/react-query"
import { Wand2 } from "lucide-react"

import { useEnterSubmit } from "@/lib/hooks/use-enter-submit"
import { useForm } from "@/lib/hooks/use-form"
import { useLocalStorage } from "@/lib/hooks/use-local-storage"
import { useScrollAnchor } from "@/lib/hooks/use-scroll-anchor"
import { cn } from "@/lib/utils"

import { EmptyScreen } from "./empty-screen"
import { Button } from "./ui/button"
import { Separator } from "./ui/seperator"
import { Textarea } from "./ui/textarea"

export default function ClientForm({
  declarationType,
  id,
}: {
  declarationType: string
  id: string
}) {
  const [isInitialMessage, setIsInitialMessage] = useState(true)
  const { state, dispatch } = useForm()
  const [nextQuestion, setNextQuestion] = useState("")
  const [sentence, setSentence] = useState("")

  const [storedChatId, setNewChatId] = useLocalStorage("newChatId", id)

  useEffect(() => {
    setNewChatId(id)
  })

  // Fetch chat history on initial load if a conversation ID exists
  const { data: restoredChat, status } = useQuery({
    queryKey: ["restoreChat", storedChatId],
    queryFn: async () => {
      if (!storedChatId) return null
      const response = await fetch(
        `http://192.168.137.185:49234//restore-chat?conversationId=${storedChatId}`
      )
      if (!response.ok) throw new Error("Failed to restore chat")
      return response.json()
    },
    refetchOnWindowFocus: false,
    retry: false,
    refetchOnMount: false,
    enabled: !!storedChatId, // Only run the query if storedChatId exists
  })

  useEffect(() => {
    if (restoredChat && status === "success") {
      dispatch({ type: "SET_RESPONSE_DATA", payload: restoredChat.formModel })

      // Update messages state with the chat log
      const messages = restoredChat.chatLog.map((message) => ({
        id: message.timeStamp.toString(), // Using timestamp as a unique id
        content: message.content,
        role: message.role,
        timestamp: new Date(message.timeStamp * 1000).toISOString(),
      }))
      dispatch({ type: "SET_MESSAGES", payload: messages })

      // Set the next question as the last assistant message, if any
      const lastAssistantMessage = restoredChat.chatLog
        .filter((msg) => msg.role.toLowerCase() === "assistant")
        .pop()
      if (lastAssistantMessage) {
        setNextQuestion(lastAssistantMessage.content)
      }

      setIsInitialMessage(false)
    }
  }, [restoredChat, dispatch, status])

  // Use useQuery for the initial message
  const { mutate: sendInitialMessage } = useMutation({
    mutationFn: async () => {
      const response = await fetch(
        "http://192.168.137.185:49234/ask-tax-assistant",
        {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify({
            userMessage: sentence,
            declarationType,
            isInitialMessage: true,
            ConversationId: id,
          }),
        }
      )
      return response.json()
    },
    onSuccess: (data) => {
      console.log(data)

      dispatch({ type: "SET_RESPONSE_DATA", payload: data.formState })
      setNextQuestion(data.nextQuestion)
      setIsInitialMessage(false)
    },
    onError: (error) => {
      console.log(error)
      //   example error {
      //     "declarationType": "OTHER",
      //     "message": "Niestety, Twoja sprawa nie jest obecnie obsługiwana przez czat. Prosimy spróbować ponownie w przyszłości."
      // }
      dispatch({ type: "SET_ERROR", payload: error.message })
    },
  })

  const { formRef, onKeyDown } = useEnterSubmit()

  const handleSubmit = async () => {
    if (isInitialMessage) {
      sendInitialMessage(undefined, {
        onSuccess: (data) => {
          console.log(data)

          dispatch({ type: "SET_RESPONSE_DATA", payload: data.formState })
          setNextQuestion(data.nextQuestion)
          setIsInitialMessage(false)
        },
      })
    }
  }

  const { messagesRef, scrollRef, visibilityRef } = useScrollAnchor()

  return (
    <div
      className="group w-full grid-cols-subgrid grid-rows-[1fr,auto] h-[calc(100dvh_-_120.8px)] overflow-hidden pl-0 peer-[[data-state=open]]:lg:pl-[250px] peer-[[data-state=open]]:xl:pl-[300px]"
      ref={scrollRef}
    >
      <div className="h-[100dvh] overflow-auto pb-[200px] pt-4 md:pt-10">
        {state.messages.length ? (
          <div className="relative mx-auto max-w-2xl px-4">
            {state.messages.map((message, index) => (
              <div key={message.id}>
                {message.display}
                {index < state.messages.length - 1 && (
                  <Separator className="my-4" />
                )}
              </div>
            ))}
          </div>
        ) : (
          <EmptyScreen />
        )}
        <div className="w-full h-px mb-auto" ref={visibilityRef} />
        <div className="relative bottom-0 w-full bg-gradient-to-b from-muted/30 from-0% to-muted/30 to-50% duration-300 ease-in-out animate-in dark:from-background/10 dark:from-10% dark:to-background/80">
          <div className="mx-auto sm:max-w-2xl sm:px-4">
            <div className="space-y-4 border-t bg-background px-4 py-2 shadow-lg sm:rounded-t-xl sm:border md:py-4">
              <form
                ref={formRef}
                onSubmit={(e) => {
                  e.preventDefault()
                  handleSubmit()
                }}
                className="flex flex-row space-x-2"
              >
                <Textarea
                  value={sentence}
                  onChange={(e) => setSentence(e.target.value)}
                  onKeyDown={onKeyDown}
                  placeholder="Type your message..."
                  className="flex-1 resize-none"
                  rows={1}
                />
                <Button type="submit" size="icon">
                  <Wand2 className="h-4 w-4" />
                  <span className="sr-only">Send message</span>
                </Button>
              </form>
            </div>
          </div>
        </div>
      </div>
      {/* <div className="relative flex h-[calc(100dvh_-_120.8px)] flex-col">
      
      <div className="flex-1 overflow-hidden">
        <div className="relative h-full overflow-auto">
          <div className="pb-[200px] pt-4 md:pt-10">
            <div className="relative mx-auto max-w-2xl px-4">
              {nextQuestion && (
                <>
                  <div className="group relative mb-4 flex items-start md:-ml-12">
                    <div className="flex h-8 w-8 shrink-0 select-none items-center justify-center rounded-md border shadow-sm">
                      AI
                    </div>
                    <div className="ml-4 flex-1 space-y-2 overflow-hidden px-1">
                      <p className="text-sm text-muted-foreground">
                        {nextQuestion}
                      </p>
                    </div>
                  </div>
                  <hr className="my-4" />
                </>
              )}
            </div>
          </div>
        </div>
      </div>
      <div className="absolute inset-x-0 bottom-0 w-full bg-gradient-to-b from-muted/30 from-0% to-muted/30 to-50% duration-300 ease-in-out animate-in dark:from-background/10 dark:from-10% dark:to-background/80">
        <div className="mx-auto sm:max-w-2xl sm:px-4">
          <div className="space-y-4 border-t bg-background px-4 py-2 shadow-lg sm:rounded-t-xl sm:border md:py-4">
            <form
              ref={formRef}
              onSubmit={(e) => {
                e.preventDefault()
                handleSubmit()
              }}
              className="flex flex-row space-x-2"
            >
              <Textarea
                value={sentence}
                onChange={(e) => setSentence(e.target.value)}
                onKeyDown={onKeyDown}
                placeholder="Type your message..."
                className="flex-1 resize-none"
                rows={1}
              />
              <Button type="submit" size="icon">
                <Wand2 className="h-4 w-4" />
                <span className="sr-only">Send message</span>
              </Button>
            </form>
          </div>
        </div>
      </div>
    </div> */}
    </div>
  )
}
