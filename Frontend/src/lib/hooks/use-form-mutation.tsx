"use client"

import { useMutation } from "@tanstack/react-query"

import { useForm } from "./use-form"

export const useFormMutation = () => {
  const { state, dispatch } = useForm()
  const mutateForm = useMutation({
    mutationFn: async () => {
      const response = await fetch("http://192.168.137.185:49234/update-form", {
        method: "POST",
        body: JSON.stringify(state.formData),
      })
      return response.json()
    },
    onSuccess: (data) => {
      dispatch({ type: "UPDATE_FORM", payload: data })
    },
  })
  return mutateForm
}
