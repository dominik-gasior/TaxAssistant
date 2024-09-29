export function EmptyScreen() {
  return (
    <div className="mx-auto max-w-2xl px-4">
      <div className="flex flex-col gap-2 rounded-lg border bg-background p-8">
        <h1 className="text-lg font-semibold uppercase">Urzędowy ChatBot.</h1>
        <p className="text-sm text-muted-foreground/85">
          Szanowny Użytkowniku, Pragniemy poinformować, że nasz czat umożliwia
          uzupełnienie formularza poprzez interaktywną konwersację. Dzięki tej
          funkcji możesz łatwo i szybko dostarczyć nam niezbędne informacje.
          Zachęcamy do skorzystania z tej opcji, aby usprawnić proces
          komunikacji.
        </p>
        {/* Hi, im  */}
        <p></p>
        {/* on the lesft side there are inputs you can fill by yourself if youd prefer */}
      </div>
    </div>
  )
}
