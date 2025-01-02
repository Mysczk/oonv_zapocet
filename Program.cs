/// <summary>
/// Abstract Factory
/// </summary>
public interface IAbstraktFactorka
{
    IPiti NalejPiti();
    IJidlo NaservirujJidlo();
}
public interface IPiti
{
    void Tecu();
}
public interface IJidlo
{
    void Jsemsolidni();
}

public class Kafe : IPiti
{
    public void Tecu()
    {
        Console.WriteLine("Kafe tece");
    }
}
public class Monsert : IPiti
{
    public void Tecu()
    {
        Console.WriteLine("Monsert tece");
    }
}

public class Pizza : IJidlo
{
    public void Jsemsolidni()
    {
        Console.WriteLine("Pizza je solidni");
    }
}

public class Hamburger : IJidlo
{
    public void Jsemsolidni()
    {
        Console.WriteLine("Hamburger je solidni");
    }
}

public class KafePizzaFactory : IAbstraktFactorka
{
    public IPiti NalejPiti()
    {
        return new Kafe();
    }
    public IJidlo NaservirujJidlo()
    {
        return new Pizza();
    }
}

public class KafeHamburgerFactory : IAbstraktFactorka
{
    public IPiti NalejPiti()
    {
        return new Kafe();
    }
    public IJidlo NaservirujJidlo()
    {
        return new Hamburger();
    }
}

public class MonsertPizzaFactory : IAbstraktFactorka
{
    public IPiti NalejPiti()
    {
        return new Monsert();
    }
    public IJidlo NaservirujJidlo()
    {
        return new Pizza();
    }
}

public class MonsertHamburgerFactory : IAbstraktFactorka
{
    public IPiti NalejPiti()
    {
        return new Monsert();
    }
    public IJidlo NaservirujJidlo()
    {
        return new Hamburger();
    }
}

/// <summary>
/// Chain of Responsibility
/// </summary>
public interface IHandler
{
    public IHandler SetNext(IHandler handler);
    public void Handle(string request);
}

public abstract class Handler : IHandler
{
    private IHandler? _next;
    public IHandler SetNext(IHandler handler)
    {
        _next = handler;
        return _next;
    }
    public virtual void Handle(string request)
    {
        if (_next != null)
        {
            _next.Handle(request);
        }
        else
        {
            System.Console.WriteLine($"Tu alfa handler a tohle ti delat nebudeme: {request}");
        }
    }
}

public class KafePizzaHandler : Handler
{
    IAbstraktFactorka KafePizzaF = new KafePizzaFactory();
    public override void Handle(string request)
    {
        if (request == "KafePizza")
        {
            KafePizzaF.NalejPiti().Tecu();
            KafePizzaF.NaservirujJidlo().Jsemsolidni();
        }
        else
        {
            base.Handle(request);
        }
    }
}
public class KafeHamburgerHandler : Handler
{
    IAbstraktFactorka KafeHamburgerF = new KafeHamburgerFactory();
    public override void Handle(string request)
    {
        if (request == "KafeHamburger")
        {
            KafeHamburgerF.NalejPiti().Tecu();
            KafeHamburgerF.NaservirujJidlo().Jsemsolidni();
        }
        else
        {
            base.Handle(request);
        }
    }
}
public class MonsertPizzaHandler : Handler
{
    IAbstraktFactorka MonsertPizzaF = new MonsertPizzaFactory();
    public override void Handle(string request)
    {
        if (request == "MonsertPizza")
        {
            MonsertPizzaF.NalejPiti().Tecu();
            MonsertPizzaF.NaservirujJidlo().Jsemsolidni();
        }
        else
        {
            base.Handle(request);
        }
    }
}
public class MonsertHamburgerHandler : Handler
{
    IAbstraktFactorka MonsertHamburgerF = new MonsertHamburgerFactory();
    public override void Handle(string request)
    {
        if (request == "MonsertHamburger")
        {
            MonsertHamburgerF.NalejPiti().Tecu();
            MonsertHamburgerF.NaservirujJidlo().Jsemsolidni();
        }
        else
        {
            base.Handle(request);
        }
    }
}
/// <summary>
/// Adapter
/// </summary>
public interface IJsonConvertor
{
    public List<string> Convert(List<string> json);
}
public class JsonConvertor : IJsonConvertor
{
    public JsonConvertor() { }

    static List<string> VycucejData(List<string> json)
    {
        return json;
    }

    public List<string> Convert(List<string> json)
    {
        return VycucejData(json);

    }
}
/// <summary>
/// Body
/// </summary>
public class Program
{
    public static void Main()
    {
        IJsonConvertor jsonConvertor = new JsonConvertor();
        List<string> jsonString = new List<string> { "KafePizza", "KafeHamburger", "MonsertPizza", "MonsertHamburger" };

        List<string> requests = jsonConvertor.Convert(jsonString);

        IHandler handler = new KafePizzaHandler();
        handler.SetNext(new KafeHamburgerHandler()).SetNext(new MonsertPizzaHandler()).SetNext(new MonsertHamburgerHandler());

        foreach (var request in requests)
        {
            handler.Handle(request);
            System.Console.WriteLine();
        }
    }
}