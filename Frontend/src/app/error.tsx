'use client' // Error components must be Client Components

import { useRouter } from 'next/navigation'
import { startTransition, useEffect } from 'react'

export default function Error({
  error,
  reset,
}: {
  error: Error & { digest?: string }
  reset: () => void
}) {
  // useEffect(() => {
  //   // Log the error to an error reporting service
  //   console.error(error)
  // }, [error])
  
  const router = useRouter()

  function refreshAndReset() {
    startTransition(() => {
      router.refresh()
      reset()
    })
  }

  return (
    <div className="grid items-center justify-center py-12 text-center md:py-24">
      <div className=" grid justify-items-center gap-8">
        <p>There has been an error. Please try again.</p>
        {error && (
          <p>
            Error messsage: <code>{error.message}</code>
          </p>
        )}
        <button
          className="mx-auto w-fit rounded bg-text-1/10 px-4 py-2 font-bold text-text-1 hover:bg-text-1/20"
          onClick={refreshAndReset}
        >
          Reload page
        </button>
        <p>If the issue persists, please report it on our Discord server.</p>
      </div>
    </div>
  )
}
