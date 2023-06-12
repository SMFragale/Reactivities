using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        // Commands don't need a return type therefore no parameter is given to the IRequest interface
        public class Command : IRequest {
            public Activity Activity { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.Activities.Add(request.Activity);
                await _context.SaveChangesAsync();

                //Equivalent to nothing
                return Unit.Value;
            }
        }
    }
}