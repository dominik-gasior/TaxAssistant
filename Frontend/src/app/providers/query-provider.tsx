"use client";

import { QueryClient, QueryClientProvider,  dehydrate,
  HydrationBoundary, } from "@tanstack/react-query";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import { ReactNode, useState } from "react";

export default function QueryProvider({ children }: { children: ReactNode }) {
  const [queryClient] = useState(() => new QueryClient());

  return (
    <QueryClientProvider client={queryClient}>
      <HydrationBoundary state={dehydrate(queryClient)}>

        {children}
        <ReactQueryDevtools initialIsOpen={false} />
      </HydrationBoundary>
    </QueryClientProvider>

  );
}
