using System.Linq.Expressions;

namespace Plaground.Infrastructure.Persistence;
public static class PredicateExtensions
{
    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>>? left,
        Expression<Func<T, bool>> right)
    {
        if (left is null)
            return right;

        return Combine(left, right, Expression.AndAlso);
    }

    public static Expression<Func<T, bool>> Or<T>(
        this Expression<Func<T, bool>>? left,
        Expression<Func<T, bool>> right)
    {
        if (left is null)
            return right;

        return Combine(left, right, Expression.OrElse);
    }

    private static Expression<Func<T, bool>> Combine<T>(
        Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right,
        Func<Expression, Expression, BinaryExpression> merge)
    {
        var param = left.Parameters[0];

        var rightBody = new ReplaceParameterVisitor(
            right.Parameters[0],
            param
        ).Visit(right.Body);

        var body = merge(left.Body, rightBody!);

        return Expression.Lambda<Func<T, bool>>(body, param);
    }

    private sealed class ReplaceParameterVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _source;
        private readonly ParameterExpression _target;

        public ReplaceParameterVisitor(
            ParameterExpression source,
            ParameterExpression target)
        {
            _source = source;
            _target = target;
        }

        protected override Expression VisitParameter(ParameterExpression node)
            => node == _source ? _target : base.VisitParameter(node);
    }
}