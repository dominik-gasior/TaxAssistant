"use client"

import { useMutation } from "@tanstack/react-query"

import { useForm } from "./use-form"

export const useFormMutation = () => {
  const { state, dispatch } = useForm()
  console.log("state", state)
  const mutateForm = useMutation({
    mutationFn: async (nanoId: string | null) => {
      console.log("nanoId", nanoId)
      if (!nanoId) {
        throw new Error("Nano ID is required")
      }
      const response = await fetch(
        `http://192.168.137.185:49234/update-form/${nanoId}`,
        {
          method: "PUT",
          body: JSON.stringify(state.formData),
        }
      )
      return response.json()
    },
    onSuccess: (data) => {
      dispatch({ type: "UPDATE_FORM", payload: data })
    },
  })
  return mutateForm
}
