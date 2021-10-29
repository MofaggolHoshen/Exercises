# jQuery API in Blazor

## Bind 'onclick' event

* Using id

```JavaScript
export function setElementText2() {
    $('#ClickMeButton').click(function () {
        alert("Handler for .click() called.");
    })
};
```

```C#
@implements IAsyncDisposable
@inject IJSRuntime JS

<div @ref="elementReference" id="ClickMeButton">Click Me</div>

@code {
    private IJSObjectReference module;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            module = await JS.InvokeAsync<IJSObjectReference>("import", "./js/script.js");
            flip();
        }
    }

    async void flip()
    {
        await module.InvokeVoidAsync("setElementText2");
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        try
        {
            if (module is not null)
            {
                await module.DisposeAsync();
            }
        }catch(Exception e)
        {

        }
    }
}

```

* Using ElementReference

```JavaScript
export function setElementText2(element) {
    $('#'+element.id).click(function () {
        alert("Handler for .click() called.");
    })
};
```

```C#
@implements IAsyncDisposable
@inject IJSRuntime JS

<div @ref="elementReference" id="ClickMeButton">Click Me</div>

@code {
    [Parameter]
    public Vocabulary Vocabulary { get; set; }

    private IJSObjectReference module;
    private ElementReference elementReference;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            module = await JS.InvokeAsync<IJSObjectReference>("import", "./js/script.js");
            flip();
        }
    }

    async void flip()
    {
        //await module.InvokeVoidAsync("flipper");

        await module.InvokeVoidAsync("setElementText2", elementReference);
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        try
        {
        if (module is not null)
        {
            await module.DisposeAsync();
        }
        }catch(Exception e)
        {
                
        }
    }
}

```
